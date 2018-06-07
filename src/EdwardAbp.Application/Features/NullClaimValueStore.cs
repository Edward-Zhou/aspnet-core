using System.Threading.Tasks;

namespace EdwardAbp.Features
{
    public class NullClaimValueStore: IClaimValueStore
    {
        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        public static NullClaimValueStore Instance { get { return SingletonInstance; } }
        private static readonly NullClaimValueStore SingletonInstance = new NullClaimValueStore();

        /// <inheritdoc/>
        public Task<string> GetValueOrNullAsync(int tenantId, Claim feature)
        {
            return Task.FromResult((string)null);
        }

        public Task<string> GetValueOrNullAsync(int tenantId, long? organizationUnitId, Claim claim)
        {
            throw new System.NotImplementedException();
        }
    }
}