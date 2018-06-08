using Abp;
using Abp.Dependency;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EdwardAbp.Features
{
    public class ClaimChecker: IClaimChecker, ITransientDependency
    {
        /// <summary>
        /// Reference to the current session.
        /// </summary>
        public ICustomAbpSession AbpSession { get; set; }

        /// <summary>
        /// Reference to the store used to get feature values.
        /// </summary>
        public IClaimValueStore ClaimValueStore { get; set; }

        private readonly IClaimManager _claimManager;

        /// <summary>
        /// Creates a new <see cref="FeatureChecker"/> object.
        /// </summary>
        public ClaimChecker(IClaimManager  claimManager)
        {
            _claimManager = claimManager;

            ClaimValueStore = NullClaimValueStore.Instance;
            //AbpSession = NullAbpSession.Instance;
        }

        /// <inheritdoc/>
        public Task<string> GetValueAsync(string name)
        {
            if (!AbpSession.TenantId.HasValue)
            {
                throw new AbpException("FeatureChecker can not get a feature value by name. TenantId is not set in the IAbpSession!");
            }

            return GetValueAsync(AbpSession.TenantId.Value, name);
        }

        /// <inheritdoc/>
        public async Task<string> GetValueAsync(int tenantId, string name)
        {
            var feature = _claimManager.Get(name);

            var value = "Tets";//= await FeatureValueStore.GetValueOrNullAsync(tenantId, feature);
            if (value == null)
            {
                return feature.DefaultValue;
            }

            return value;
        }

        public Task<string> GetValueAsync(int tenantId, long? organizationUnitId, string name)
        {
            throw new NotImplementedException();
        }
    }
}
