// -----------------------------------------------------------------------
// <copyright file="IFaker.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Test.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// The IFaker is a interface used by test object fakers.
    /// </summary>
    public interface IFaker<T>
    {
        /// <summary>
        /// Generates this instance.
        /// </summary>
        /// <returns>An instance of T.</returns>
        T Generate();

        /// <summary>
        /// Generates the specified count.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>A list of T.</returns>
        List<T> Generate(int count);
    }
}