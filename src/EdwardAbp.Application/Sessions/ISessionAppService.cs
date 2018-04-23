using System.Threading.Tasks;
using Abp.Application.Services;
using EdwardAbp.Sessions.Dto;

namespace EdwardAbp.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
