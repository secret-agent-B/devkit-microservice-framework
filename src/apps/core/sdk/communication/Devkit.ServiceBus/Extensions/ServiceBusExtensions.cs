// -----------------------------------------------------------------------
// <copyright file="ServiceBusExtensions.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus.Extensions
{
    using System;
    using System.IO;
    using Devkit.ServiceBus.Interfaces;
    using Devkit.ServiceBus.Services;
    using MassTransit;
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
        /// <typeparam name="TConsumerRegistry">The type of the consumer registrar.</typeparam>
        /// <param name="services">The services.</param>
        /// <returns>
        /// The service collection.
        /// </returns>
        public static IServiceCollection AddServiceBus<TConsumerRegistry>(this IServiceCollection services)
            where TConsumerRegistry : class, IBusRegistry
        {
            // Adding this into the integration test as middleware will cause the test to stop responding.
            services.AddSingleton<IHostedService, BusHostedService>();
            services.AddSingleton<IBusRegistry, TConsumerRegistry>();

            var serviceBusType = Environment.GetEnvironmentVariable("SERVICE_BUS_TYPE");
            var provider = services.BuildServiceProvider();

            switch (serviceBusType.ToLower())
            {
                case "amqp":
                    services.UseAMQPServiceBus();
                    break;

                default:
                    // no service bus
                    break;
            }

            return services;
        }

        /// <summary>
        /// Ins the memory service bus.
        /// </summary>
        /// <param name="services">The services.</param>
        private static void UseAMQPServiceBus(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                var provider = services.BuildServiceProvider();
                var registry = provider.GetRequiredService<IBusRegistry>();

                registry.RegisterConsumers(x);
                registry.RegisterRequestClients(x);

                // Add bus to the container.
                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile($"infrastructure/bus-amqp.{environment}.json", false, false)
                        .Build();

                    var options = configuration.GetSection(ServiceBusConfiguration.Section).Get<ServiceBusConfiguration>();

                    cfg.Host(
                        new Uri($"amqp://{options.Host}"),
                        hostConfig =>
                        {
                            hostConfig.Username(options.Username);
                            hostConfig.Password(options.Password);
                            hostConfig.Heartbeat(options.Heartbeat);
                        });

                    // Configure the endpoints for all defined consumer, saga, and activity types using an optional
                    // endpoint name formatter. If no endpoint name formatter is specified and an
                    // MassTransit.IEndpointNameFormatter is registered in the container, it is resolved from the container.
                    // Otherwise, the MassTransit.Definition.DefaultEndpointNameFormatter is used.
                    cfg.ConfigureEndpoints(context, new EndpointNameFormatter());
                }));
            });

            // Needed for transfering files within the ecosystem.
            services.AddSingleton<IMessageDataRepository, InMemoryMessageDataRepository>();
        }
    }
}