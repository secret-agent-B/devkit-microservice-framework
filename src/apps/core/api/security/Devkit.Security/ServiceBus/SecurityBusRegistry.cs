// -----------------------------------------------------------------------
// <copyright file="SecurityConsumerRegistry.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.ServiceBus
{
    using System.Diagnostics.CodeAnalysis;
    using Devkit.ServiceBus.Interfaces;
    using MassTransit;
    using MassTransit.ExtensionsDependencyInjectionIntegration;

    /// <summary>
    /// The SecurityConsumerRegistry contains consumer registration instructions for the Security API.
    /// </summary>
    /// <seealso cref="IBusRegistry" />
    public class SecurityBusRegistry : IBusRegistry
    {
        /// <summary>
        /// Configure message consumers.
        /// </summary>
        /// <param name="configurator">The configurator.</param>
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Parameter provided by DevKit.")]
        public void RegisterConsumers(IServiceCollectionBusConfigurator configurator)
        {
            configurator.AddConsumersFromNamespaceContaining<SecurityBusRegistry>();
        }

        /// <summary>
        /// Registers the request clients.
        /// </summary>
        /// <param name="configurator">The configurator.</param>
        public void RegisterRequestClients(IServiceCollectionBusConfigurator configurator)
        {
        }
    }
}