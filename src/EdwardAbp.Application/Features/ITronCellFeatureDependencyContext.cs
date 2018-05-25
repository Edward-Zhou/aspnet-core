using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdwardAbp.Features
{
    public interface ITronCellFeatureDependencyContext
    {
        int? TenantId { get; }
        long? OrganizationUnitId { get; }
        IIocResolver IocResolver { get; }
        ITronCellFeatureChecker TronCellFeatureChecker { get; }
    }
}
