using Abp.Configuration.Startup;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using Abp.Runtime;
using Abp.Runtime.Session;
using EdwardAbp.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdwardAbp
{
    public interface ICustomAbpSession: IAbpSession
    {
        long? OrganizationUnitId { get; }
        long? ImpersonatorOrganizationUnitId { get; }
        //IDisposable Use(int? tenantId, long? ImpersonatorOrganizationUnitId, long? userId);
    }
    public class CustomAbpSession : ClaimsAbpSession, ICustomAbpSession
    {
        private readonly IRepository<User, long> _userRepository;
        public CustomAbpSession(IRepository<User, long> userRepository,
            IPrincipalAccessor principalAccessor,
            IMultiTenancyConfig multiTenancy,
            ITenantResolver tenantResolver,
            IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider):base(principalAccessor, multiTenancy, tenantResolver, sessionOverrideScopeProvider)
        {
            _userRepository = userRepository;
        }
        public virtual long? OrganizationUnitId
        {
            get {
                if (UserId != null)
                {
                    var user = _userRepository.Get(UserId.Value);
                    return 4;
                }
                return null;
            }
        }

        public long? ImpersonatorOrganizationUnitId => throw new NotImplementedException();

        //public IDisposable Use(int? tenantId, long? organizationUnitId, long? userId)
        //{
        //    //TenantId = tenantId;
        //    //OrganizationUnitId = organizationUnitId;
        //    //UserId = userId;
        //    return IDisposab
        //}
    }
}
