// -----------------------------------------------------------------------
// <copyright file="QueryResponseSet.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.CQRS.Query
{
    using System.Collections.Generic;

    /// <summary>
    /// A response that is used for returning collections to the caller.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <seealso cref="ResponseBase" />
    public class QueryResponseSet<TResponse> : ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryResponseSet{TResponse}"/> class.
        /// </summary>
        public QueryResponseSet()
        {
            this.Items = new List<TResponse>();
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public List<TResponse> Items { get; }
    }
}