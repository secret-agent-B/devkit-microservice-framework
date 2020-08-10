// -----------------------------------------------------------------------
// <copyright file="AuthorizationPolicy.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Gateway.Configuration
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The authorization policy that defines what the gateway needs to authorize a request.
    /// </summary>
    [SuppressMessage("Design", "CA1812: Avoid uninstantiated internal classes", Justification = "Used for configuration.")]
    internal class AuthorizationPolicy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationPolicy"/> class.
        /// </summary>
        public AuthorizationPolicy()
        {
            this.ClaimRequirements = new HashSet<ClaimRequirement>();
        }

        /// <summary>
        /// Gets or sets the claim requirements.
        /// </summary>
        /// <value>
        /// The claim requirements.
        /// </value>
        public ICollection<ClaimRequirement> ClaimRequirements { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}