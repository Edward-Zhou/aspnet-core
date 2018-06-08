using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EdwardAbp.EntityFrameworkCore.Repositories
{
    public interface ICustomRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IEntity<int>
    {
        Task BulkInsertAsync(IList<TEntity> entities);
    }
    public class CustomRepository<TEntity> : EdwardAbpRepositoryBase<TEntity, int>, ICustomRepository<TEntity, int>, IRepository<TEntity, int>,
                IRepository<TEntity>
    where TEntity : class, IEntity<int>
    {
        public CustomRepository(IDbContextProvider<EdwardAbpDbContext> dbContextProvider)
        : base(dbContextProvider)
        {
        }
        public async Task BulkInsertAsync(IList<TEntity> entities)
        {
            using (var transaction = Context.Database.BeginTransaction())
            {
                await Context.BulkInsertAsync(entities);
                transaction.Commit();
            }
        }
    }

    public interface ICustomRepository<TEntity, TPrimaryKey>: IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        Task BulkInsertAsync(IList<TEntity> entities);
    }
    public class CustomRepository<TEntity, TPrimaryKey> : EdwardAbpRepositoryBase<TEntity, TPrimaryKey>, 
        ICustomRepository<TEntity, TPrimaryKey>, 
        IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public CustomRepository(IDbContextProvider<EdwardAbpDbContext> dbContextProvider)
        : base(dbContextProvider)
        {
        }
        public async Task BulkInsertAsync(IList<TEntity> entities)
        {

            await Context.BulkInsertAsync(entities);
        }
    }
}
