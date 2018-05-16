using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using EdwardAbp.Authorization.Roles;
using EdwardAbp.Authorization.Users;
using EdwardAbp.MultiTenancy;
using System;
using System.Linq.Expressions;
using Abp.Organizations;
using Abp.Domain.Uow;
using Microsoft.EntityFrameworkCore.Metadata;
using Abp;
using Abp.Domain.Entities;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Abp.Events.Bus.Entities;

namespace EdwardAbp.EntityFrameworkCore
{
    public class EdwardAbpDbContext : AbpZeroDbContext<Tenant, Role, User, EdwardAbpDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public EdwardAbpDbContext(DbContextOptions<EdwardAbpDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<PType> PType { get; set; }

        public ICustomAbpSession CustomAbpSession => AbpSession as ICustomAbpSession;

        public virtual bool SuppressAutoSetOrganizationUnitId { get; set; }
        protected virtual long? CurrentOrganizationUnitId => GetCurrentOrganizationUnitIdOrNull();

        protected virtual long? GetCurrentOrganizationUnitIdOrNull()
        {
            //if (CurrentUnitOfWorkProvider != null &&
            //    CurrentUnitOfWorkProvider.Current != null)
            //{
            //    return ((CustomActiveUnitOfWork)CurrentUnitOfWorkProvider.Current).GetOrganizationUnitId();
            //}
            return CustomAbpSession.OrganizationUnitId;
        }
        protected virtual bool IsMayHaveOrganizationUnitFilterEnabled => CurrentOrganizationUnitId != null && CurrentUnitOfWorkProvider?.Current?.IsFilterEnabled("MayHaveOrganizationUnit") == true;

        protected override bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType)
        {
            if (typeof(IMayHaveOrganizationUnit).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }
            return base.ShouldFilterEntity<TEntity>(entityType);
        }
        protected override Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
        {
            Expression<Func<TEntity, bool>> expression = null;
            if (typeof(IMayHaveOrganizationUnit).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> mayHaveOUFilter = e => ((IMayHaveOrganizationUnit)e).OrganizationUnitId == CurrentOrganizationUnitId 
                || (((IMayHaveOrganizationUnit)e).OrganizationUnitId == CurrentOrganizationUnitId) == IsMayHaveOrganizationUnitFilterEnabled;
                expression = expression == null ? mayHaveOUFilter : CombineExpressions(expression, mayHaveOUFilter);
            }

            expression = expression == null ? base.CreateFilterExpression<TEntity>() : CombineExpressions(expression, base.CreateFilterExpression<TEntity>());

            return expression;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Product>().HasQueryFilter(b => EF.Property<long?>(b, "OrganizationUnitId") == CurrentOrganizationUnitId);
        }
        protected override void ApplyAbpConceptsForAddedEntity(EntityEntry entry, long? userId, EntityChangeReport changeReport)
        {
            CheckAndSetMayHaveTenantIdProperty(entry.Entity);
            base.ApplyAbpConceptsForAddedEntity(entry, userId, changeReport);
        }

        protected virtual void CheckAndSetMayHaveTenantIdProperty(object entityAsObj)
        {
            if (SuppressAutoSetTenantId)
            {
                return;
            }

            //Only works for single tenant applications
            if (MultiTenancyConfig?.IsEnabled ?? false)
            {
                return;
            }

            //Only set IMayHaveTenant entities
            if (!(entityAsObj is IMayHaveTenant))
            {
                return;
            }

            var entity = entityAsObj.As<IMayHaveTenant>();

            //Don't set if it's already set
            if (entity.TenantId != null)
            {
                return;
            }

            entity.TenantId = GetCurrentTenantIdOrNull();
        }

    }
}
