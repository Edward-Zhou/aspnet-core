﻿using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using Dapper;
using EFCore.BulkExtensions;
using System.Collections.Generic;
using System.Data;
using static Dapper.SqlMapper;

namespace EdwardAbp.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Base class for custom repositories of the application.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public abstract class EdwardAbpRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<EdwardAbpDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected EdwardAbpRepositoryBase(IDbContextProvider<EdwardAbpDbContext> dbContextProvider)
            : base(dbContextProvider)
        {  
            
        }

        protected virtual GridReader QueryMultiple(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QueryMultiple(sql, param, transaction, commandTimeout, commandType);
        }



        //protected virtual 

        // Add your common methods for all repositories

    }

    /// <summary>
    /// Base class for custom repositories of the application.
    /// This is a shortcut of <see cref="EdwardAbpRepositoryBase{TEntity,TPrimaryKey}"/> for <see cref="int"/> primary key.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class EdwardAbpRepositoryBase<TEntity> : EdwardAbpRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected EdwardAbpRepositoryBase(IDbContextProvider<EdwardAbpDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        // Do not add any method here, add to the class above (since this inherits it)!!!
    }
}
