using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdwardAbp.Features
{
    public class TronCellFeatureDependencyContext:ITronCellFeatureDependencyContext, ITransientDependency
    {
        public int? TenantId { get; set; }
        public long? OrganizationUnitId { get; }

        /// <inheritdoc/>
        public IIocResolver IocResolver { get; private set; }

        /// <inheritdoc/>
        public ITronCellFeatureChecker TronCellFeatureChecker { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureDependencyContext"/> class.
        /// </summary>
        public TronCellFeatureDependencyContext(IIocResolver iocResolver, ITronCellFeatureChecker featureChecker)
        {
            IocResolver = iocResolver;
            TronCellFeatureChecker = featureChecker;
        }

    }
}
