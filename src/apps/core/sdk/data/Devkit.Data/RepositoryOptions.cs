// -----------------------------------------------------------------------
// <copyright file="RepositoryOptions.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Data
{
    /// <summary>
    /// The repository configuration.
    /// </summary>
    public class RepositoryOptions
    {
        /// <summary>
        /// The configuration section.
        /// </summary>
        internal const string _section = "RepositoryOptions";

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public string DatabaseName { get; set; }
    }
}