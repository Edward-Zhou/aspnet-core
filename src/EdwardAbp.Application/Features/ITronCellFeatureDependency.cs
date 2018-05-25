using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EdwardAbp.Features
{
    interface ITronCellFeatureDependency
    {
        Task<bool> IsSatisfiedAsync(ITronCellFeatureDependencyContext context);
    }
}
