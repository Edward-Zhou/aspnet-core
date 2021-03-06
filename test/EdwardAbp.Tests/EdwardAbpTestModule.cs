using System;
using Castle.MicroKernel.Registration;
using NSubstitute;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Modules;
using Abp.Configuration.Startup;
using Abp.Net.Mail;
using Abp.TestBase;
using Abp.Zero.Configuration;
using Abp.Zero.EntityFrameworkCore;
using EdwardAbp.EntityFrameworkCore;
using EdwardAbp.Tests.DependencyInjection;
using Abp.Runtime.Session;

namespace EdwardAbp.Tests
{
    [DependsOn(
        typeof(EdwardAbpApplicationModule),
        typeof(EdwardAbpEntityFrameworkModule),
        typeof(AbpTestBaseModule)
        )]
    public class EdwardAbpTestModule : AbpModule
    {
        public EdwardAbpTestModule(EdwardAbpEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;
        }

        public override void PreInitialize()
        {
       
            //Configuration.IocManager.IocContainer.Register(
            //       Component.For<IAbpSession>().ImplementedBy<CustomTestSession>().LifestyleSingleton()
            //    );
            //Configuration.IocManager.Register<IAbpSession, CustomTestSession>(DependencyLifeStyle.Singleton);
            Configuration.UnitOfWork.Timeout = TimeSpan.FromMinutes(30);
            Configuration.UnitOfWork.IsTransactional = false;

            // Disable static mapper usage since it breaks unit tests (see https://github.com/aspnetboilerplate/aspnetboilerplate/issues/2052)
            Configuration.Modules.AbpAutoMapper().UseStaticMapper = false;

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;

            // Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            RegisterFakeService<AbpZeroDbMigrator<EdwardAbpDbContext>>();

            Configuration.ReplaceService<IEmailSender, NullEmailSender>(DependencyLifeStyle.Transient);
            
        }

        public override void Initialize()
        {
            ServiceCollectionRegistrar.Register(IocManager);
            //Configuration.ReplaceService<IAbpSession, CustomTestSession>(DependencyLifeStyle.Singleton);

        }

        private void RegisterFakeService<TService>() where TService : class
        {
            IocManager.IocContainer.Register(
                Component.For<TService>()
                    .UsingFactoryMethod(() => Substitute.For<TService>())
                    .LifestyleSingleton()
            );
        }
    }
}
