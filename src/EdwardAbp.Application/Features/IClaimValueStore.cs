using System.Threading.Tasks;

namespace EdwardAbp.Features
{
    public interface IClaimValueStore
    {
        Task<string> GetValueOrNullAsync(int tenantId, long? organizationUnitId, Claim claim);
    }
}