// -----------------------------------------------------------------------
// <copyright file="OcelotExtensions.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Gateway.Extensions
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Ocelot.Configuration.File;

    /// <summary>
    /// Ocelot extensions.
    /// </summary>
    public static class OcelotExtensions
    {
        /// <summary>
        /// Adds the ocelot.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="gatewayType">Type of the gateway.</param>
        /// <param name="environmentName">Name of the environment.</param>
        /// <returns>
        /// The configuration builder.
        /// </returns>
        public static IConfigurationBuilder AddOcelot(this IConfigurationBuilder builder, string folder, string gatewayType, string environmentName)
        {
            const string globalConfigFile = "ocelot.global.json";
            const string subConfigPattern = @"^ocelot\.[a-zA-Z0-9]+\.json$";

            var ocelotConfiguration = $"ocelot.{gatewayType}.json";
            var excludeConfigName = string.IsNullOrEmpty(environmentName)
                ? $"ocelot.{environmentName}.json"
                : string.Empty;

            var reg = new Regex(subConfigPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var fileConfiguration = new FileConfiguration();

            var gatewayFiles = new DirectoryInfo(folder)
                .EnumerateFiles()
                .Where(fi => reg.IsMatch(fi.Name) && (fi.Name != excludeConfigName))
                .ToList();

            var commonFiles = new DirectoryInfo(folder)
                .EnumerateFiles()
                .Where(fi => reg.IsMatch(fi.Name) && (fi.Name != excludeConfigName))
                .ToList();

            foreach (var file in gatewayFiles)
            {
                if (gatewayFiles.Count > 1 && file.Name.Equals(ocelotConfiguration, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var lines = File.ReadAllText(file.FullName);
                var config = JsonConvert.DeserializeObject<FileConfiguration>(lines);

                if (file.Name.Equals(globalConfigFile, StringComparison.OrdinalIgnoreCase))
                {
                    fileConfiguration.GlobalConfiguration = config.GlobalConfiguration;
                }

                fileConfiguration.Aggregates.AddRange(config.Aggregates);
                fileConfiguration.Routes.AddRange(config.Routes);
            }

            var json = JsonConvert.SerializeObject(fileConfiguration);

            File.WriteAllText(ocelotConfiguration, json);

            builder.AddJsonFile(ocelotConfiguration, false, false);
            builder.AddJsonFile(Path.Combine(folder, gatewayType), true, false);

            return builder;
        }
    }
}