// -----------------------------------------------------------------------
// <copyright file="ISeederConfig.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Data.Interfaces
{
    /// <summary>
    /// The seeder config contract.
    /// </summary>
    public interface ISeederConfig
    {
        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        string FilePath { get; set; }
    }
}