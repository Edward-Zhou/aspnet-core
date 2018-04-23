using System.Threading.Tasks;
using Abp.Application.Services;
using EdwardAbp.Authorization.Accounts.Dto;

namespace EdwardAbp.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
