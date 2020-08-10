// -----------------------------------------------------------------------
// <copyright file="IResponse.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.CQRS.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    /// The response contract.
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// Gets the exceptions.
        /// </summary>
        /// <value>
        /// The exceptions.
        /// </value>
        IDictionary<string, IEnumerable<string>> Exceptions { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is successful.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is successful; otherwise, <c>false</c>.
        /// </value>
        bool IsSuccessful { get; }
    }
}