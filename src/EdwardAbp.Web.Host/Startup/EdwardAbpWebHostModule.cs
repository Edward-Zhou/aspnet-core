using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using EdwardAbp.Configuration;

namespace EdwardAbp.Web.Host.Startup
{
    [DependsOn(
       typeof(EdwardAbpWebCoreModule))]
    public class EdwardAbpWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public EdwardAbpWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EdwardAbpWebHostModule).GetAssembly());
        }
    }
}
