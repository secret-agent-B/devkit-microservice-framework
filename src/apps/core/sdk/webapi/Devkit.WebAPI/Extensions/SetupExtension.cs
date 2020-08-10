// -----------------------------------------------------------------------
// <copyright file="SetupExtension.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.WebAPI.Extensions
{
    using Devkit.WebAPI;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// The setup extension methods.
    /// </summary>
    public static class SetupExtension
    {
        /// <summary>
        /// Gets the API definition.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns>An API definition.</returns>
        public static APIDefinition GetAPIDefinition(this IConfiguration config)
        {
            var apiDefinition = new APIDefinition();
            config.Bind("APIDefinition", apiDefinition);

            return apiDefinition;
        }
    }
}