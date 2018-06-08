using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using EdwardAbp.Authorization;
using EdwardAbp.EntityFrameworkCore.Repositories;

namespace EdwardAbp
{
    [DependsOn(
        typeof(EdwardAbpCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class EdwardAbpApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<EdwardAbpAuthorizationProvider>();
        }


        public override void Initialize()
        {
            var thisAssembly = typeof(EdwardAbpApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
