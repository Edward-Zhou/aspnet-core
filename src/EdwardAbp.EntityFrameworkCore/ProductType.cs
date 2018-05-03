using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdwardAbp
{
    public class ProductType : FullAuditedEntity
    {
        public string Name { get; set; }
        public bool IsChangeAble { get; set; }
        public List<Product> Products { get; set; }
        
    }
    public class PType : FullAuditedEntity, IMayHaveTenant
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public virtual int? TenantId { get; set; }
    }
}
