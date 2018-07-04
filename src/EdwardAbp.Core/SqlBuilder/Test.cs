using System;
using System.Collections.Generic;
using System.Text;

namespace EdwardAbp.SqlBuilder
{
    class Test
    {
        static void Main(string[] args)
        {
            //var sb = new SqlBuilder(new StringBuilder());
            //var sb = new StringBuilder();
            //sb.AppendLineIf("Hello World");
            //sb.AppendLineIf("");
            //sb.AppendLineIf("Will you go?");
            //sb.AppendLineIf("WUXI");
            //var test = "Test";
            //sb.AppendLine($" { ( 1==2 ? test : "{}" )} ");

            //Console.WriteLine(sb.ToString());
            Console.ReadLine();
        }
        public static StringBuilder FiltersBase(FilterModel filter)
        {
            var sql = new StringBuilder();
            sql.AppendLine("And IsDeleted = 0")
               .AppendLineIf(filter.TenantId.HasValue, $"And TenantId = { filter.TenantId }")
               .AppendLineIf(filter.Filter != null, $"And Title like '%{ filter.Filter }%'")
               .AppendLineIf(filter.AuditStatus.HasValue, $"And AuditStatus = { filter.AuditStatus }");
            return sql;
        }
        public static StringBuilder PagedBase(FilterModel filter)
        {
            var sql = new StringBuilder();

            sql.AppendLineIf(filter.Sorting != null, $"Order By { filter.Sorting }")
               .AppendLine($"OFFSET { filter.SkipCount } Rows Fetch Next { filter.MaxResultCount } Rows Only;");
            return sql;
        }
        public static StringBuilder Base(StringBuilder builder, FilterModel filter)
        {
            //var sql = new StringBuilder();
            builder.AppendLine($@"
            	Select Id From (
		            Select Id,OrganizationUnitId From Coupons
		            Union
		            Select CouponId, OrganizationUnitId From DispatchedCoupon
                ) as T
	            --OuId Filter based on Tenant, tenant is null, otherwise is currentOu
            ")
                .AppendLineIf(filter.OrganizationUnitId.HasValue, $"Where OrganizationUnitId = { filter.OrganizationUnitId }", $"Where OrganizationUnitId is Null");
            return builder;
        }
        //public static StringBuilder Count(StringBuilder builder, FilterModel filter)
        //{
        //    var sql = new StringBuilder();
        //    sql.AppendLine(@"Select * From Coupons
        //                    Where Id in 
        //                    (")
        //       .Base(filter)
        //       .AppendLine("Where Id in");
        //}
        //public static string CouponsAll(FilterModel filter)
        //{
        //    var sql = Base(filter)
        //              .
        //}

    }
    static class Extensions
    {

        public static StringBuilder AppendLineIf(this StringBuilder builder, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return builder;
            }
            return builder.AppendLine(value);
        }
        public static StringBuilder AppendLineIf(this StringBuilder builder, bool isTrue, string trueValue, string falseValue = null)
        {
            if (isTrue)
            {
                return builder.AppendLine(trueValue);
            }
            return builder.AppendLine(falseValue);
        }
    }

}
