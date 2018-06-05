using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EdwardAbp.Features
{
    public interface IClaimDependency
    {
        Task<bool> IsSatisfiedAsync(IClaimDependencyContext context);
    }
}
