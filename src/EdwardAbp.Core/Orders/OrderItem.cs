using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdwardAbp.Orders
{
    public class OrderItem : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        //public string OuterId { get; set; }
        public long OrderId { get; set; }

        public virtual Order Order { get; set; }
        /// <summary>
        /// 所属Tenant.
        /// </summary>
        public int TenantId { get; set; }

        public long? OrganizationUnitId { get; set; }
        /// <summary>
        /// Sku 编号
        /// </summary>
        public string SkuId { get; set; }
        /// <summary>
        /// Sku名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Sku图片地址
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public double TotalFee { get; set; }
        /// <summary>
        /// 税金
        /// </summary>
        public double TaxFee { get; set; }
        /// <summary>
        /// 实付金额
        /// </summary>
        public double Payment { get; set; }
        /// <summary>
        /// Sku状态
        /// </summary>
        public string Status { get; set; }

    }
}
