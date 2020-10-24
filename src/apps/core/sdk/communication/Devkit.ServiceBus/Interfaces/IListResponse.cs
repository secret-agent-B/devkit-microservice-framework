// -----------------------------------------------------------------------
// <copyright file="IListResponse.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// The IListResponse represents collection of T.
    /// </summary>
    /// <typeparam name="T">Underlying type of the response.</typeparam>
    public interface IListResponse<T>
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        IList<T> Items { get; }
    }
}