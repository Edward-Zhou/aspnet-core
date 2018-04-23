using Abp.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdwardAbp
{
    public interface IEfUnitOfWorkFilterExecuter : IUnitOfWorkFilterExecuter
    {
        void ApplyCurrentFilters(IUnitOfWork unitOfWork, DbContext dbContext);
    }
}
