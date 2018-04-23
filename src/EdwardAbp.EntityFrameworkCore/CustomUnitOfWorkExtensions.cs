using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdwardAbp
{
        public static class CustomUnitOfWorkExtensions
    {
            /// <summary>
            /// Gets a DbContext as a part of active unit of work.
            /// This method can be called when current unit of work is an <see cref="EfCoreUnitOfWork"/>.
            /// </summary>
            /// <typeparam name="TDbContext">Type of the DbContext</typeparam>
            /// <param name="unitOfWork">Current (active) unit of work</param>
            public static TDbContext GetDbContext<TDbContext>(this IActiveUnitOfWork unitOfWork)
                where TDbContext : DbContext
            {
                return GetDbContext<TDbContext>(unitOfWork, null);
            }

            public static TDbContext GetDbContext<TDbContext>(this IActiveUnitOfWork unitOfWork, MultiTenancySides? multiTenancySide)
                where TDbContext : DbContext
            {
                if (unitOfWork == null)
                {
                    throw new ArgumentNullException("unitOfWork");
                }

                if (!(unitOfWork is CustomActiveUnitOfWork))
                {
                    throw new ArgumentException("unitOfWork is not type of " + typeof(CustomActiveUnitOfWork).FullName, "unitOfWork");
                }

                return (unitOfWork as CustomActiveUnitOfWork).GetOrCreateDbContext<TDbContext>(multiTenancySide);
            }
        }

    }
