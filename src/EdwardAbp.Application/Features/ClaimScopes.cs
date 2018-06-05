using System;

namespace EdwardAbp.Features
{
    [Flags]
    public enum ClaimScopes
    {
        /// <summary>
        /// This <see cref="Feature"/> can be enabled/disabled per edition.
        /// </summary>
        Edition = 1,

        /// <summary>
        /// This Feature<see cref="Feature"/> can be enabled/disabled per tenant.
        /// </summary>
        OrganizationUnit = 2,

        /// <summary>
        /// This <see cref="Feature"/> can be enabled/disabled per tenant and edition.
        /// </summary>
        All = 3

    }
}