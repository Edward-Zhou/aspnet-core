using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Uow;
using Abp.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdwardAbp
{
    public class CustomUnitOfWorkDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
    where TDbContext : DbContext
    {
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkDbContextProvider{TDbContext}"/>.
        /// </summary>
        /// <param name="currentUnitOfWorkProvider"></param>
        public CustomUnitOfWorkDbContextProvider(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }

        public TDbContext GetDbContext()
        {
            return GetDbContext(null);
        }

        public TDbContext GetDbContext(MultiTenancySides? multiTenancySide)
        {
            return _currentUnitOfWorkProvider.Current.GetDbContext<TDbContext>(multiTenancySide);
        }
    }

}
