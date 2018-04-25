using Abp.Configuration.Startup;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using Abp.Runtime;
using Abp.Runtime.Session;
using EdwardAbp.Authorization.Users;
using EdwardAbp.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdwardAbp
{
    public interface ICustomAbpSession: IAbpSession
    {
        long? OrganizationUnitId { get; }
        long? ImpersonatorOrganizationUnitId { get; }
        IDisposable Use(int? tenantId, long? organizationUnitId, long? userId);
    }
    public class CustomAbpSession : ClaimsAbpSession, ICustomAbpSession
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IAmbientScopeProvider<CustomSessionOverride> _customSessionOverrideScopeProvider;
        public CustomAbpSession(IRepository<User, long> userRepository,
            IPrincipalAccessor principalAccessor,
            IMultiTenancyConfig multiTenancy,
            ITenantResolver tenantResolver,
            IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider,
            IAmbientScopeProvider<CustomSessionOverride> customSessionOverrideScopeProvider) :base(principalAccessor, multiTenancy, tenantResolver, sessionOverrideScopeProvider)
        {
            _userRepository = userRepository;
            CustomSessionOverrideScopeProvider = customSessionOverrideScopeProvider;
        }
        protected new CustomSessionOverride OverridedValue => CustomSessionOverrideScopeProvider.GetValue("CustomSessionOverride");
        protected IAmbientScopeProvider<CustomSessionOverride> CustomSessionOverrideScopeProvider { get; }

        public virtual long? OrganizationUnitId
        {
            get {
                if (OverridedValue != null)
                {
                    return (OverridedValue).OrganizationUnitId;
                }

                var tenantIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == "OU");
                if (!string.IsNullOrEmpty(tenantIdClaim?.Value))
                {
                    return Convert.ToInt32(tenantIdClaim.Value);
                }
                return null;
            }
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
