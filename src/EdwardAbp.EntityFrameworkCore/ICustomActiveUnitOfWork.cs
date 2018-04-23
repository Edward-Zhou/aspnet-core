using Abp;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFramework;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Uow;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdwardAbp
{
    public class CustomActiveUnitOfWork: EfCoreUnitOfWork, IActiveUnitOfWork
    {
        public CustomActiveUnitOfWork(
                    IIocResolver iocResolver,
                    IConnectionStringResolver connectionStringResolver,
                    IUnitOfWorkFilterExecuter filterExecuter,
                    IDbContextResolver dbContextResolver,
                    IUnitOfWorkDefaultOptions defaultOptions,
                    IDbContextTypeMatcher dbContextTypeMatcher,
                    IEfCoreTransactionStrategy transactionStrategy)
                    : base(iocResolver, 
                          connectionStringResolver, filterExecuter, dbContextResolver, defaultOptions,  dbContextTypeMatcher,
                          transactionStrategy)
        { }
        private long? _organizationUnitId;
        public long? GetOrganizationUnitId()
        {
            return _organizationUnitId;
        }
        public virtual IDisposable SetOrganizationUnitId(int? organizationUnitId)
        {
            _organizationUnitId = organizationUnitId;
            return new DisposeAction(() =>
            {
            });
        }

    }
}
