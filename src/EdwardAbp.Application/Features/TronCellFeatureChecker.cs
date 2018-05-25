using Abp;
using Abp.Collections.Extensions;
using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EdwardAbp.Features
{
    public class TronCellFeatureChecker : ITronCellFeatureChecker, ITransientDependency
    {
        public ICustomAbpSession CustomAbpSession { get; set; }

        public TronCellFeatureChecker()
        {

        }
        public Task<string> GetValueAsync(string name)
        {
            if (!CustomAbpSession.TenantId.HasValue)
            {
                throw new AbpException("FeatureChecker can not get a feature value by name. TenantId is not set in the IAbpSession!");
            }
            if (!CustomAbpSession.OrganizationUnitId.HasValue)
            {
                throw new AbpException("FeatureChecker can not get a feature value by name. TenantId is not set in the IAbpSession!");
            }
            return GetValueAsync(CustomAbpSession.TenantId.Value, CustomAbpSession.OrganizationUnitId.Value, name);
        }

        public async Task<string> GetValueAsync(int tenantId, long? organizationUnitId, string name)
        {
            return "Hello";
        }

        public async Task<bool> IsEnabledAsync(string featureName)
        {
            return string.Equals(await GetValueAsync(featureName), "true", StringComparison.OrdinalIgnoreCase);
        }
        public async Task<bool> IsEnabledAsync(int tenantId, long? organizationUnitId, string featureName)
        {
            return string.Equals(await GetValueAsync(tenantId, organizationUnitId, featureName), "true", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<bool> IsEnabled(int tenantId, long? organizationUnitId, bool requiresAll, params string[] featureNames)
        {
            if (featureNames.IsNullOrEmpty())
            {
                return true;
            }

            if (requiresAll)
            {
                foreach (var featureName in featureNames)
                {
                    if (!(await IsEnabledAsync(tenantId, organizationUnitId, featureName)))
                    {
                        return false;
                    }
                }

                return true;
            }

            foreach (var featureName in featureNames)
            {
                if (await IsEnabledAsync(tenantId, organizationUnitId, featureName))
                {
                    return true;
                }
            }

            return false;

        }

        public Task<bool> IsEnabledAsync(int tenantId, long? organizationUnitId, bool requiresAll, params string[] featureNames)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEnabledAsync(bool requiresAll, string[] features)
        {
            throw new NotImplementedException();
        }
    }
}
