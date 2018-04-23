using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EdwardAbp.MultiTenancy.Dto;

namespace EdwardAbp.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
