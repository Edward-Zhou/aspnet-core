using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EdwardAbp.Features
{
    public class TronCellFeatureDependency: ITronCellFeatureDependency
    {
        public string[] Features { get; set; }
        public bool RequiresAll { get; set; }
        public TronCellFeatureDependency(params string[] features)
        {
            Features = features;
        }
        public TronCellFeatureDependency(bool requiresAll, params string[] features)
                    : this(features)
        {
            RequiresAll = requiresAll;
        }

        public Task<bool> IsSatisfiedAsync(ITronCellFeatureDependencyContext context)
        {
            return context.TenantId.HasValue
                ? context.TronCellFeatureChecker.IsEnabledAsync(context.TenantId.Value,context.OrganizationUnitId, RequiresAll, Features)
                : context.TronCellFeatureChecker.IsEnabledAsync(RequiresAll, Features);
        }


    }
}
