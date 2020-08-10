// -----------------------------------------------------------------------
// <copyright file="ServiceBusExtensions.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus.Extensions
{
    using System;
    using Devkit.ServiceBus.Interfaces;
    using Devkit.ServiceBus.Services;
    using MassTransit;
    using MassTransit.ExtensionsDependencyInjectionIntegration;
    using MassTransit.MessageData;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// The service bus extensions class.
    /// </summary>
    public static class ServiceBusExtensions
    {
        /// <summary>
        /// Uses the service bus.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configurator">The configurator.</param>
        /// <param name="useInMemoryServiceBus">if set to <c>true</c> [use in memory service bus].</param>
        /// <returns>
        /// The service collection.
        /// </returns>
        public static IServiceCollection AddServiceBus(this IServiceCollection services, Action<IServiceCollectionBusConfigurator> configurator, bool useInMemoryServiceBus)
        {
            _ = bool.TryParse(Environment.GetEnvironmentVariable("DISABLE_SERVICE_BUS"), out var disableMiddleware);

            if (disableMiddleware)
            {
                return services;
            }

            // Get the json configuration and use it to setup connection to RabbitMQ.
            var serviceBusOptions = services
            .BuildServiceProvider()
            .GetRequiredService<IConfiguration>()
            .GetSection(ServiceBusOptions.Section)
            .Get<ServiceBusOptions>();

            if (serviceBusOptions == null)
            {
                return services;
            }

            if (!useInMemoryServiceBus)
            {
                // In Memory service bus will be configured on the Shared Test project and not here.
                // Setup RabbitMQ connection and logging to serilog.
                // This will also use the configurator to setup subscriptions to the service bus. Beep beep!!!
                services.UseRabbitMQServiceBus(serviceBusOptions, configurator);

                // TODO: We'll need to set this up and not just use the default settings for encryption. Need to think about where to get certs too.
                // services.AddSingleton<IMessageDataRepository, FileSystemMessageDataRepository>();
                // services.AddSingleton<IMessageDataRepository, EncryptedMessageDataRepository>();

                // Adding this into the integration test as middleware will cause the test to stop responding.
                services.AddSingleton<IHostedService, BusHostedService>();
            }

            services.AddSingleton<IMessageDataRepository, InMemoryMessageDataRepository>();
            services.AddSingleton<IServiceBusClient, ServiceBusClient>();

            return services;
        }

        /// <summary>
        /// Setups the specified services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="serviceBusOptions">The service bus options.</param>
        /// <param name="configurator">The configurator.</param>
        private static void UseRabbitMQServiceBus(this IServiceCollection services, ServiceBusOptions serviceBusOptions, Action<IServiceCollectionBusConfigurator> configurator)
        {
            // UseInMemoryServiceBus DI for MassTransit.
            services.AddMassTransit(x =>
            {
                configurator(x);

                // Add bus to the container.
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(
                        new Uri($"amqp://{serviceBusOptions.Host}"),
                        hostConfig =>
                        {
                            hostConfig.Username(serviceBusOptions.Username);
                            hostConfig.Password(serviceBusOptions.Password);
                            hostConfig.Heartbeat(serviceBusOptions.Heartbeat);
                        });

                    // Configure the endpoints for all defined consumer, saga, and activity types using an optional
                    // endpoint name formatter. If no endpoint name formatter is specified and an
                    // MassTransit.IEndpointNameFormatter is registered in the container, it is resolved from the container.
                    // Otherwise, the MassTransit.Definition.DefaultEndpointNameFormatter is used.
                    cfg.ConfigureEndpoints(provider, new EndpointNameFormatter());
                }));
            });
        }
    }
}