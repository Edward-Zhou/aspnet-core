using System;
using System.Collections.Generic;
using System.Text;

namespace EdwardAbp.SqlBuilder
{
    public class SqlBuilder
    {
        public StringBuilder Sql { get; set; }
        private StringBuilder _builder;
        public SqlBuilder()
        {
        }
        public SqlBuilder(FilterModel builder)
        {
            //_builder = builder;
        }

        //public virtual SqlBuilder FilterBase(FilterModel filter)
        //{
        //    //var builder = new StringBuilder();
        //    _builder.AppendLine("And IsDeleted = 0")
        //       .AppendLineIf(filter.TenantId.HasValue, $"And TenantId = { filter.TenantId }")
        //       .AppendLineIf(filter.Filter != null, $"And Title like '%{ filter.Filter }%'")
        //       .AppendLineIf(filter.AuditStatus.HasValue, $"And AuditStatus = { filter.AuditStatus }");
        //    return new SqlBuilder(_builder);
        //}

        //public virtual SqlBuilder PagedBase(FilterModel filter)
        //{
        //    //var builder = new StringBuilder();
        //    _builder.AppendLineIf(filter.Sorting != null, $"Order By { filter.Sorting }")
        //       .AppendLine($"OFFSET { filter.SkipCount } Rows Fetch Next { filter.MaxResultCount } Rows Only;");
        //    return new SqlBuilder(_builder);
        //}

        //public virtual SqlBuilder Base(FilterModel filter)
        //{
        //    //var builder = new StringBuilder();
        //    _builder.AppendLine($@"
        //    	Select Id From (
		      //      Select Id,OrganizationUnitId From Coupons
		      //      Union
		      //      Select CouponId, OrganizationUnitId From DispatchedCoupon
        //        ) as T
	       //     --OuId Filter based on Tenant, tenant is null, otherwise is currentOu
        //    ")
        //        .AppendLineIf(filter.OrganizationUnitId.HasValue, $"Where OrganizationUnitId = { filter.OrganizationUnitId }", $"Where OrganizationUnitId is Null");
        //    return new SqlBuilder(_builder);
        //}

        //public virtual SqlBuilder Count(FilterModel filter)
        //{
        //    var sql = new SqlBuilder(new StringBuilder());
        //    sql.Base(filter)
        //       .FilterBase(filter)
        //       .PagedBase(filter);
        //    return sql;
        //}

        //public string BuildSql(FilterModel filter)
        //{
        //    return _builder.ToString();
        //}
    }
}
