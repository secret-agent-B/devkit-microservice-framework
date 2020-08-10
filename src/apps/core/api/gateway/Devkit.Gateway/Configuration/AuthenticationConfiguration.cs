// -----------------------------------------------------------------------
// <copyright file="AuthenticationConfiguration.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Gateway.Configuration
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The authentication configuration that are used for gateway and security API introspection.
    /// </summary>
    [SuppressMessage("Design", "CA1812: Avoid uninstantiated internal classes", Justification = "Used for configuration.")]
    internal class AuthenticationConfiguration
    {
        /// <summary>
        /// Gets or sets the name of the gateway API.
        /// </summary>
        /// <value>
        /// The name of the gateway API.
        /// </value>
        public string APIResourceName { get; set; }

        /// <summary>
        /// Gets or sets the API secret.
        /// </summary>
        /// <value>
        /// The API secret.
        /// </value>
        public string APIResourceSecret { get; set; }

        /// <summary>
        /// Gets or sets the name of the API.
        /// !!!IMPORTANT!!! This is equivalent to the Ocelot (configuration)'s AuthenticationProviderKey.
        /// This tells Ocelot which authentication provider to use to validate the JWT token.
        /// </summary>
        /// <value>
        /// The name of the API.
        /// </value>
        public string AuthenticationProviderKey { get; set; }

        /// <summary>
        /// Gets or sets the cache duration in minutes.
        /// </summary>
        /// <value>
        /// The cache duration in minutes.
        /// </value>
        public int CacheDurationInMinutes { get; set; }

        /// <summary>
        /// Gets or sets the authority.
        /// !!!IMPORTANT!!! This value is the host URL for where the identity server is hosted.
        /// This is where the gateway pass the JWT token to for introspection.
        /// </summary>
        /// <value>
        /// The authority.
        /// </value>
        public string IdentityServerHost { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [require HTTPS metadata].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [require HTTPS metadata]; otherwise, <c>false</c>.
        /// </value>
        public bool RequireHttpsMetadata { get; set; }

        /// <summary>
        /// Gets or sets the type of the role claim.
        /// </summary>
        /// <value>
        /// The type of the role claim.
        /// </value>
        public string RoleClaimType { get; set; }
    }
}