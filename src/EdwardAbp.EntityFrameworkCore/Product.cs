using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EdwardAbp
{
    [Table("AbpProducts")]
    public class Product : FullAuditedEntity<long>, IMayHaveTenant, IMayHaveOrganizationUnit
    {
        public virtual int? TenantId { get; set; }
        public virtual long? OrganizationUnitId { get; set; }
        public string Name { get; set; }
        public int? ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        //public string  Type { get; set; }
        public int? PTypeId { get; set; }
        public PType PType { get; set; }

    }
}
