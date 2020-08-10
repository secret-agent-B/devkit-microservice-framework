// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Gateway
{
    using System;
    using System.IO;
    using Devkit.Gateway.Extensions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// The program runtime class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>A host builder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var ocelotConfigPath = Environment.GetEnvironmentVariable("OCELOT_CONFIG_PATH");
            var gatewayType = Environment.GetEnvironmentVariable("DEVKIT_GATEWAY_TYPE");
            var ocelotConfigs = Path.Combine(ocelotConfigPath, gatewayType);

            var hostBuilder = Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    })
                .ConfigureAppConfiguration(
                    (context, configBuilder) =>
                    {
                        configBuilder
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddOcelot(ocelotConfigs, gatewayType, context.HostingEnvironment.EnvironmentName)
                            .AddJsonFile($"configs/{gatewayType}/appsettings.json", false, true)
                            .AddJsonFile($"configs/{gatewayType}/appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);
                    });

            return hostBuilder;
        }

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
    }
}