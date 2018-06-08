using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdwardAbp.EntityFrameworkCore
{
    //
    // Summary:
    //     This interface is implemented by all repositories to ensure implementation of
    //     fixed methods.
    //
    // Type parameters:
    //   TEntity:
    //     Main Entity type this repository works on
    //
    //   TPrimaryKey:
    //     Primary key type of the entity
    //public interface IRepository<TEntity, TPrimaryKey> 
    //{
    //    static void InsertBluck(this IRepository<TEntity, TPrimaryKey> repository, List<TEntity> entities);
    //}
    public static class Extension
    {
        //public static void InsertBluck<TEntity>(this IRepository repository, List<TEntity> entities)
        //{
        //    using (var transaction = Context.Database.BeginTransaction())
        //    {
        //        Context.BulkInsert(entities);
        //        transaction.Commit();
        //    }

        //}
    }
}
