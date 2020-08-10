// -----------------------------------------------------------------------
// <copyright file="ResponseSet.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.CQRS
{
    using System.Collections.Generic;

    /// <summary>
    /// A response that represents a set of T.
    /// </summary>
    /// <typeparam name="T">Type of set.</typeparam>
    public class ResponseSet<T> : ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseSet{T}"/> class.
        /// </summary>
        public ResponseSet()
        {
            this.Items = new List<T>();
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public IEnumerable<T> Items { get; set; }
    }
}