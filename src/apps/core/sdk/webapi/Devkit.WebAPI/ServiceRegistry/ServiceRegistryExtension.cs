// -----------------------------------------------------------------------
// <copyright file="ServiceRegistryExtension.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.WebAPI.ServiceRegistry
{
    using System;
    using Consul;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// The service registry extension method class.
    /// </summary>
    internal static class ServiceRegistryExtension
    {
        /// <summary>
        /// Adds the service registry.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>A service collection.</returns>
        internal static IServiceCollection AddServiceRegistry(this IServiceCollection services)
        {
            _ = bool.TryParse(Environment.GetEnvironmentVariable("DISABLE_SERVICE_REGISTRY"), out var disableMiddleware);

            if (disableMiddleware)
            {
                return services;
            }

            var provider = services.BuildServiceProvider();
            var configuration = provider.GetService<IConfiguration>();
            var consulServiceConfig = configuration.GetSection(ConsulServiceConfiguration._section).Get<ConsulServiceConfiguration>();

            services.AddSingleton(consulServiceConfig);
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                consulConfig.Datacenter = consulServiceConfig.DataCenter;
                consulConfig.Address = new Uri(consulServiceConfig.ConsulHost);
            }));

            services.AddSingleton<IHostedService, ConsulHostedService>();

            return services;
        }
    }
}