using System;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using EdwardAbp.Authentication.JwtBearer;
using EdwardAbp.Configuration;
using EdwardAbp.EntityFrameworkCore;
using Abp.Runtime.Session;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Uow;
using Castle.MicroKernel.Registration;
using Abp.EntityFrameworkCore;
using EdwardAbp.EntityFrameworkCore.Repositories;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching.Redis;

#if FEATURE_SIGNALR
using Abp.Web.SignalR;
#elif FEATURE_SIGNALR_ASPNETCORE
using Abp.AspNetCore.SignalR;
#endif

namespace EdwardAbp
{
    [DependsOn(
         typeof(EdwardAbpApplicationModule),
         typeof(EdwardAbpEntityFrameworkModule),
         typeof(AbpAspNetCoreModule),
         typeof(AbpRedisCacheModule)
#if FEATURE_SIGNALR 
        ,typeof(AbpWebSignalRModule)
#elif FEATURE_SIGNALR_ASPNETCORE
        ,typeof(AbpAspNetCoreSignalRModule)
#endif
     )]
    public class EdwardAbpWebCoreModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public EdwardAbpWebCoreModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                EdwardAbpConsts.ConnectionStringName
            );
            //Configuration.ReplaceService(typeof(IActiveUnitOfWork), () =>
            //{
            //    IocManager.Register<IActiveUnitOfWork, CustomActiveUnitOfWork>(DependencyLifeStyle.Transient);
            //});
            Configuration.Caching.Configure("Product", cache => {
                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(2);
            });
            Configuration.ReplaceService(typeof(IUnitOfWork), () =>
            {
                IocManager.Register<IUnitOfWork, CustomActiveUnitOfWork>(DependencyLifeStyle.Transient);
            });
            IocManager.IocContainer.Register(
            Component.For(typeof(ICustomRepository<>))
                .ImplementedBy(typeof(CustomRepository<>))
                .LifestyleTransient()
            );
            IocManager.IocContainer.Register(
            Component.For(typeof(ICustomRepository<,>))
                .ImplementedBy(typeof(CustomRepository<,>))
                .LifestyleTransient()
            );

            IocManager.IocContainer.Register(
            Component.For(typeof(IDbContextProvider<>))
                .ImplementedBy(typeof(CustomUnitOfWorkDbContextProvider<>))
                .LifestyleTransient()
            );

            Configuration.UnitOfWork.RegisterFilter("MayHaveOrganizationUnit", true);
            Configuration.ReplaceService(typeof(IAbpSession), () =>
            {
                IocManager.Register<IAbpSession, CustomAbpSession>(DependencyLifeStyle.Transient);
            });


            // Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(EdwardAbpApplicationModule).GetAssembly()
                 );

            ConfigureTokenAuth();
        }

        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();
            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = TimeSpan.FromDays(1);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EdwardAbpWebCoreModule).GetAssembly());


        }
    }
}
