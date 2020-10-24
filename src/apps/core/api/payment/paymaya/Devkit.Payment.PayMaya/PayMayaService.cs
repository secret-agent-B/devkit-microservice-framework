// -----------------------------------------------------------------------
// <copyright file="PayMayaService.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Payment.Paymaya
{
    using System.Threading;
    using System.Threading.Tasks;
    using MassTransit;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// The PayMayaService is the hosted service that consumes any PayMaya payment requests.
    /// </summary>
    public class PayMayaService : IHostedService
    {
        /// <summary>
        /// The bus control.
        /// </summary>
        private readonly IBusControl _busControl;

        /// <summary>
        /// Initializes a new instance of the <see cref="PayMayaService" /> class.
        /// </summary>
        /// <param name="busControl">The bus control.</param>
        public PayMayaService(IBusControl busControl)
        {
            this._busControl = busControl;
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await this._busControl.StartAsync(cancellationToken);
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await this._busControl.StopAsync(cancellationToken);
        }
    }
}