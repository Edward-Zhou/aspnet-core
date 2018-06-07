using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdwardAbp.Features
{
    public interface IClaimDependencyContext
    {
        /// <summary>
        /// Tenant id which requires the feature.
        /// Null for current tenant.
        /// </summary>
        int? TenantId { get; }

        long? OrganizationUnitId { get; set; }

        /// <summary>
        /// Gets the <see cref="IIocResolver"/>.
        /// </summary>
        /// <value>
        /// The ioc resolver.
        /// </value>
        IIocResolver IocResolver { get; }

        /// <summary>
        /// Gets the <see cref="IFeatureChecker"/>.
        /// </summary>
        /// <value>
        /// The feature checker.
        /// </value>
        IClaimChecker ClaimChecker { get; }

    }
}
