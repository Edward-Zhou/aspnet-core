using Abp.Application.Features;
using Abp.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EdwardAbp.Features
{
    public static class TronCellExtension
    {
        public static async Task<bool> IsEnabledAsync(this IFeatureChecker featureChecker, int tenantId, long? organizationUnitId,
            bool requiresAll, params string[] featureNames)
        {
            if (!organizationUnitId.HasValue)
            {
                return await featureChecker.IsEnabledAsync(tenantId, requiresAll, featureNames);
            }
            else
            {
                if (featureNames.IsNullOrEmpty())
                {
                    return true;
                }
                if (requiresAll)
                {
                    foreach (var featureName in featureNames)
                    {
                        if (!(await featureChecker.IsEnabledAsync(tenantId, organizationUnitId, featureName)))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                foreach (var featureName in featureNames)
                {
                    if (await featureChecker.IsEnabledAsync(tenantId, organizationUnitId, featureName))
                    {
                        return true;
                    }
                }

                return false;

            }
        }

        public static async Task<bool> IsEnabledAsync(this IFeatureChecker featureChecker, int tenantId, long? organizationUnitId, string featureName)
        {
            return string.Equals(await featureChecker.GetValueAsync(tenantId, organizationUnitId, featureName), "true", StringComparison.OrdinalIgnoreCase);
        }

        public static async Task<string> GetValueAsync(this IFeatureChecker featureChecker, int tenantId, long? organizationUnitId, string name)
        {
            //var feature = featureChecker.Get(name);

            //var value = await FeatureValueStore.GetValueOrNullAsync(tenantId, feature);
            //if (value == null)
            //{
            //    return feature.DefaultValue;
            //}

            //return value;
            return "value";
        }

    }
}
