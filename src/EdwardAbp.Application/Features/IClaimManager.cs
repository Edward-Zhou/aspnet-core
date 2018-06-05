using System.Collections.Generic;

namespace EdwardAbp.Features
{
    public interface IClaimManager
    {
        /// <summary>
        /// Gets the <see cref="Feature"/> by a specified name.
        /// </summary>
        /// <param name="name">Unique name of the feature.</param>
        Claim Get(string name);

        /// <summary>
        /// Gets the <see cref="Feature"/> by a specified name or returns null if it can not be found.
        /// </summary>
        /// <param name="name">The name.</param>
        Claim GetOrNull(string name);

        /// <summary>
        /// Gets all <see cref="Feature"/>s.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<Claim> GetAll();

    }
}