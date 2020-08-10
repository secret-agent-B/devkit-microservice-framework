// -----------------------------------------------------------------------
// <copyright file="ClaimRequirement.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Gateway.Configuration
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// A claim requirement.
    /// </summary>
    [SuppressMessage("Design", "CA1812: Avoid uninstantiated internal classes", Justification = "Used for configuration.")]
    internal class ClaimRequirement
    {
        /// <summary>
        /// Gets or sets the requirements.
        /// </summary>
        /// <value>
        /// The requirements.
        /// </value>
        public string[] Requirements { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }
    }
}