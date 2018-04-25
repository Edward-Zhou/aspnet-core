using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdwardAbp.EntityFrameworkCore
{
    public class CustomSessionOverride
    {
        public long? UserId { get; }

        public int? TenantId { get; }

        public long? OrganizationUnitId { get; }
        public CustomSessionOverride(int? tenantId, long? userId, long? organizationUnitId) 
        {
            OrganizationUnitId = organizationUnitId;
            UserId = userId;
            TenantId = tenantId;
        }
    }
    //public class CustomSessionOverride : SessionOverride
    //{
    //    public long? OrganizationUnitId { get; }
    //    public CustomSessionOverride(int? tenantId, long? userId, long? organizationUnitId) : base(tenantId, userId)
    //    {
    //        OrganizationUnitId = organizationUnitId;
    //    }
    //}
}
