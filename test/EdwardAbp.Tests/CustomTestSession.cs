using Abp.Configuration.Startup;
using Abp.MultiTenancy;
using Abp.Runtime;
using Abp.Runtime.Session;
using Abp.TestBase.Runtime.Session;
using EdwardAbp.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdwardAbp.Tests
{
    public class CustomTestSession : TestAbpSession, ICustomAbpSession
    {
        private readonly IAmbientScopeProvider<CustomSessionOverride> _customSessionOverrideScopeProvider;
        public CustomTestSession(
            IMultiTenancyConfig multiTenancy,
            ITenantResolver tenantResolver,
            IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider,
            IAmbientScopeProvider<CustomSessionOverride> customSessionOverrideScopeProvider) : base(multiTenancy, sessionOverrideScopeProvider, tenantResolver)
        {
            CustomSessionOverrideScopeProvider = customSessionOverrideScopeProvider;
        }
        protected new CustomSessionOverride OverridedValue => CustomSessionOverrideScopeProvider.GetValue("CustomSessionOverride");
        protected IAmbientScopeProvider<CustomSessionOverride> CustomSessionOverrideScopeProvider { get; }
        protected IPrincipalAccessor PrincipalAccessor { get; }
        private long? _organizationUnitId;
        public virtual long? OrganizationUnitId
        {
            get
            {                
                return _organizationUnitId;
            }
            set { _organizationUnitId = value; }
        }

        public long? ImpersonatorOrganizationUnitId => throw new NotImplementedException();

        //public new IDisposable Use(int? tenantId, long? userId)
        //{
        //    return SessionOverrideScopeProvider.BeginScope(SessionOverrideContextKey, new SessionOverride(tenantId, userId));
        //}

        public IDisposable Use(int? tenantId, long? organizationUnitId, long? userId)
        {
            return CustomSessionOverrideScopeProvider.BeginScope("CustomSessionOverride", new CustomSessionOverride(tenantId, userId, organizationUnitId));
        }
    }
}
