using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EdwardAbp.Features
{
    public interface ITronCellFeatureChecker
    {
        Task<string> GetValueAsync(string name);

        Task<string> GetValueAsync(int tenantId, long? organizationUnitId, string name);

        Task<bool> IsEnabledAsync(string featureName);

        Task<bool> IsEnabledAsync(int tenantId, long? organizationUnitId, string featureName);

        Task<bool> IsEnabledAsync(int tenantId, long? organizationUnitId, bool requiresAll, params string[] featureNames);
        Task<bool> IsEnabledAsync(bool requiresAll, string[] features);
    }
}
