// -----------------------------------------------------------------------
// <copyright file="LoggingExtensions.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Metrics.Extensions
{
    using System;
    using System.IO;
    using System.Reflection;
    using Devkit.Metrics.Configurations;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using Serilog.Formatting.Elasticsearch;
    using Serilog.Sinks.Elasticsearch;

    /// <summary>
    /// The LoggingExtensions provides ability to services to log to Elasticsearch.
    /// </summary>
    public static class LoggingExtension
    {
        /// <summary>
        /// Configures the logging.
        /// </summary>
        /// <param name="hostBuilder">The web host builder.</param>
        /// <returns>The web host.</returns>
        public static IHostBuilder ConfigureSerilog(this IHostBuilder hostBuilder)
        {
            SetupLogger();

            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton(Log.Logger);
            });

            return hostBuilder;
        }

        /// <summary>
        /// Configures the logging.
        /// </summary>
        /// <param name="webHostBuilder">The web host builder.</param>
        /// <returns>The web host.</returns>
        public static IWebHostBuilder ConfigureSerilog(this IWebHostBuilder webHostBuilder)
        {
            SetupLogger();

            webHostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton(Log.Logger);
            });

            return webHostBuilder;
        }

        /// <summary>
        /// Configures the default logging.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        private static void ConfigureDefaultLogging(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .ReadFrom
                .Configuration(configuration)
                .CreateLogger();
        }

        /// <summary>
        /// Configures the elasticsearch logging.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        private static void ConfigureElasticsearchLogging(IConfiguration configuration)
        {
            const string aspNetCoreEnvironmentKey = "ASPNETCORE_ENVIRONMENT";
            const string logIndexKey = "LOG_INDEX";
            const string elasticsearchConfigurationKey = "ElasticsearchConfiguration";

            var environment = Environment.GetEnvironmentVariable(aspNetCoreEnvironmentKey) ?? string.Empty;
            var logIndex = Environment.GetEnvironmentVariable(logIndexKey) ?? Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-");
            var elasticConfig = configuration.GetSection(elasticsearchConfigurationKey).Get<ElasticsearchConfiguration>();
            var node = new Uri(elasticConfig.Uri);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(node)
                {
                    AutoRegisterTemplate = true,
                    CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                    IndexFormat = $"{logIndex}-{environment.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
                })
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty(aspNetCoreEnvironmentKey, environment)
                .ReadFrom
                .Configuration(configuration)
                .CreateLogger();
        }

        /// <summary>
        /// Setups the logger.
        /// </summary>
        private static void SetupLogger()
        {
            var logSink = Environment.GetEnvironmentVariable("LOG_SINK") ?? string.Empty;

            switch (logSink)
            {
                case "elk":
                    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    ConfigureElasticsearchLogging(
                        new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile($"infrastructure/log-elk.{environment}.json", false, true)
                            .Build());
                    break;

                default:
                    ConfigureDefaultLogging(
                        new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile($"infrastructure/log-console.json", false, true)
                            .Build());
                    break;
            }

            Serilog.Debugging.SelfLog.Enable(Console.Error);
        }
    }
}