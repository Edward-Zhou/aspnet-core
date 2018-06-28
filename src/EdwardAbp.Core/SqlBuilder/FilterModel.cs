using System;
using System.Collections.Generic;
using System.Text;

namespace EdwardAbp.SqlBuilder
{
    public class FilterModel
    {
        public int? TenantId { get; set; }
        public long? OrganizationUnitId { get; set; }
        public long DeviceId { get; set; }
        public string Filter { get; set; }
        public AuditStatus? AuditStatus { get; set; }
        public string Sorting { get; set; }
        public int SkipCount { get; set; }
        public int MaxResultCount { get; set; }
    }
    public enum AuditStatus
    {
        Test,
        Hello
    }

}
