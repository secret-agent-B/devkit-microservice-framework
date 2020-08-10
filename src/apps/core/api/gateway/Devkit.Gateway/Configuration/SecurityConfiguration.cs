// -----------------------------------------------------------------------
// <copyright file="SecurityConfiguration.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Gateway.Configuration
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The security configuration.
    /// </summary>
    [SuppressMessage("Design", "CA1812: Avoid uninstantiated internal classes", Justification = "Used for configuration.")]
    internal class SecurityConfiguration
    {
        /// <summary>
        /// The configuration section.
        /// </summary>
        internal const string _section = "SecurityConfiguration";

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityConfiguration"/> class.
        /// </summary>
        public SecurityConfiguration()
        {
            this.AuthenticationConfiguration = new AuthenticationConfiguration();
        }

        /// <summary>
        /// Gets or sets the authentication options.
        /// </summary>
        /// <value>
        /// The authentication options.
        /// </value>
        public AuthenticationConfiguration AuthenticationConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the authorization policies.
        /// </summary>
        /// <value>
        /// The authorization policies.
        /// </value>
        public ICollection<AuthorizationPolicy> AuthorizationPolicies { get; set; }
    }
}