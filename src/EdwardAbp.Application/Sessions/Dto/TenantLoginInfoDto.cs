using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EdwardAbp.MultiTenancy;

namespace EdwardAbp.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
