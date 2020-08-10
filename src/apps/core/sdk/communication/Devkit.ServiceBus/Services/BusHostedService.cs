// -----------------------------------------------------------------------
// <copyright file="BusHostedService.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus.Services
{
    using System.Threading;
    using System.Threading.Tasks;
    using MassTransit;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// The bus hosted service establishes the connection to RabbitMQ through MassTransit.
    /// </summary>
    public class BusHostedService : IHostedService
    {
        /// <summary>
        /// The bus control.
        /// </summary>
        private readonly IBusControl _busControl;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusHostedService" /> class.
        /// </summary>
        /// <param name="busControl">The bus control.</param>
        public BusHostedService(IBusControl busControl)
        {
            this._busControl = busControl;
        }

        /// <summary>Triggered when the application host is ready to start the service.</summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        /// <returns>A task.</returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this._busControl.StartAsync(cancellationToken);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        /// <returns>A task.</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            this._busControl.StopAsync(cancellationToken);
            return Task.FromResult(0);
        }
    }
}