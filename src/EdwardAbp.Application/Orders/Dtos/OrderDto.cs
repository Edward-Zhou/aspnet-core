﻿using Abp.AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdwardAbp.Orders.Dtos
{
    [AutoMapTo(typeof(Order))]
    public class OrderDto
    {
        public int TenantId { get; set; } = 1;
        public long OrderId { get; set; }

        public string Number { get; set; }

        public DateTime OrderDateTime { get; set; }

        public string Status { get; set; }

        //实付金额。精确到2位小数;单位:元。如:200.07，表示:200元7分
        public double Payment { get; set; }

        //订单合计金额
        public double TotalFee { get; set; }
        //优惠金额
        public double DiscountFee { get; set; }

        //邮费。精确到2位小数;单位:元。如:200.07，表示:200元7分
        public double PostFee { get; set; }

        //天猫国际官网直供主订单关税税费
        public double TaxFee { get; set; }

        public DateTime PayTime { get; set; }

        //退款时间
        public DateTime? RefundTime { get; set; }
        public string PayType { get; set; }


        //卖家发货时间。格式:yyyy-MM-dd HH:mm:ss
        public DateTime ConsignTime { get; set; }

        //退货时间.
        public DateTime? ReturnsTime { get; set; }

        //1门店自提，总店发货，分店取货的门店自提订单标识
        public string ShopPick { get; set; }

        // 创建交易时的物流方式（交易完成前，物流方式有可能改变，但系统里的这个字段一直不变）。可选值：free(卖家包邮),post(平邮),express(快递),ems(EMS),virtual(虚拟发货)，25(次日必达)，26(预约配送)。
        public string ShippingType { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        //public Address Address { get; set; }
        public long? AddressId { get; set; }
        public IFormFile file { get; set; }

        public virtual ICollection<OrderItemDto> OrderItems { get; set; }

    }
    [AutoMapTo(typeof(OrderItem))]
    public class OrderItemDto
    {
        public int TenantId { get; set; } = 1;

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
