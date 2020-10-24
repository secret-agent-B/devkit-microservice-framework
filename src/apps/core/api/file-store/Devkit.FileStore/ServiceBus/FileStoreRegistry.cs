// -----------------------------------------------------------------------
// <copyright file="FileStoreRegistry.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.FileStore.ServiceBus
{
    using Devkit.FileStore.ServiceBus.Consumers;
    using Devkit.ServiceBus.Interfaces;
    using MassTransit;
    using MassTransit.ExtensionsDependencyInjectionIntegration;

    /// <summary>
    /// The FileStore API service bus registry.
    /// </summary>
    /// <seealso cref="IBusRegistry" />
    public class FileStoreRegistry : IBusRegistry
    {
        /// <summary>
        /// Configure message consumers.
        /// </summary>
        /// <param name="configurator">The configurator.</param>
        public void RegisterConsumers(IServiceCollectionBusConfigurator configurator)
        {
            configurator.AddConsumer<DownloadFileConsumer>();
            configurator.AddConsumer<UploadBase64ImageConsumer>();
            configurator.AddConsumer<UploadFileConsumer>();
            configurator.AddConsumer<DeleteFileConsumer>();
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