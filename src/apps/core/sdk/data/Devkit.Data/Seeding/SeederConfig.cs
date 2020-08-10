// -----------------------------------------------------------------------
// <copyright file="SeederConfig.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Data.Seeding
{
    using Devkit.Data.Interfaces;

    /// <summary>
    /// The seeder config.
    /// </summary>
    /// <seealso cref="ISeederConfig" />
    internal class SeederConfig : ISeederConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeederConfig" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public SeederConfig(string filePath)
        {
            this.FilePath = filePath;
        }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        public string FilePath { get; set; }
    }
}