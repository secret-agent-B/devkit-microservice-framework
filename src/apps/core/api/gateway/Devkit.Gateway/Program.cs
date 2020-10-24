// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Gateway
{
    using System;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using Devkit.Gateway.Extensions;
    using Devkit.Metrics.Extensions;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// The program runtime class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>An IHostBuilder.</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var ocelotConfigPath = Environment.GetEnvironmentVariable("OCELOT_CONFIG_PATH");
                    var gatewayType = Environment.GetEnvironmentVariable("DEVKIT_GATEWAY_TYPE");
                    var ocelotConfigs = Path.Combine(ocelotConfigPath, gatewayType);

                    config
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddOcelot(ocelotConfigs, gatewayType, context.HostingEnvironment.EnvironmentName)
                        .AddJsonFile($"configs/{gatewayType}/appsettings.json", false, true)
                        .AddJsonFile($"configs/{gatewayType}/appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);
                })
                .UseKestrel((context, options) =>
                {
                    options.ConfigureHttpsDefaults(httpOptions =>
                    {
                        var sslCert = Environment.GetEnvironmentVariable("SSL_CERT");

                        if (File.Exists(sslCert))
                        {
                            httpOptions.ServerCertificate = new X509Certificate2(
                                sslCert ?? throw new FileNotFoundException("SSL certificate not found.", sslCert),
                                Environment.GetEnvironmentVariable("SSL_PASSWORD")
                            );

                            options.Listen(IPAddress.Loopback, 443);
                        }

                        options.Listen(IPAddress.Loopback, 80);
                    });
                })
                .UseStartup<Startup>();

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .ConfigureSerilog()
                .Build()
                .Run();
        }
    }
}