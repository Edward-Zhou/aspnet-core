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

        public virtual bool SuppressAutoSetOrganizationUnitId { get; set; }
        protected virtual long? CurrentOrganizationUnitId => GetCurrentOrganizationUnitIdOrNull();

        protected virtual long? GetCurrentOrganizationUnitIdOrNull()
        {
            if (CurrentUnitOfWorkProvider != null &&
                CurrentUnitOfWorkProvider.Current != null)
            {
                return ((CustomActiveUnitOfWork)CurrentUnitOfWorkProvider.Current).GetOrganizationUnitId();
            }
            return ((ICustomAbpSession)AbpSession).OrganizationUnitId;
        }
        protected virtual bool IsMayHaveOrganizationUnitFilterEnabled => true;

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
                Expression<Func<TEntity, bool>> mayHaveOUFilter = e => ((IMayHaveOrganizationUnit)e).OrganizationUnitId == CurrentOrganizationUnitId || (((IMayHaveOrganizationUnit)e).OrganizationUnitId == CurrentOrganizationUnitId) == IsMayHaveOrganizationUnitFilterEnabled;
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
    }
}
