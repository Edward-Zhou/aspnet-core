using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EdwardAbp.EntityFrameworkCore.Repositories
{
    public interface ICustomRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IEntity<int>
    {
        Task BulkInsertAsync(IList<TEntity> entities);
        PagedResultDto<TEntity> PagedResult(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
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

        public Task BulkUpdateAsync(IList<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public PagedResultDto<TEntity> PagedResult(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            throw new NotImplementedException();
        }
    }

    public interface ICustomRepository<TEntity, TPrimaryKey>: IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        PagedResultDto<TEntity> PagedResult(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        Task BulkInsertAsync(IList<TEntity> entities);
        Task BulkUpdateAsync(IList<TEntity> entities);
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
        public async Task  BulkUpdateAsync(IList<TEntity> entities)
        {
            await Context.BulkUpdateAsync(entities, bulkConfig:new BulkConfig { UpdateByProperties = new List<string> { "Number","SkuId" } });
        }

        public PagedResultDto<TEntity> PagedResult(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var result = base.QueryMultiple(sql, param, Transaction, commandTimeout, commandType))
            {
                var a = result.Read().Single();
                var b = result.Read();
                var c = result.Read().ToList();
                return new PagedResultDto<TEntity>(
                           result.Read<int>().Single(),
                           result.Read<TEntity>().ToList()
                    );
            }

            //using (var trans = Connection.BeginTransaction())
            //{
            //    using (var result = base.QueryMultiple(sql, param, trans, commandTimeout, commandType))
            //    {
            //        return new PagedResultDto<TEntity>(
            //                   result.Read<int>().Single(),
            //                   result.Read<TEntity>().ToList()
            //            );
            //    }
            //}
        }
    }
}
