// -----------------------------------------------------------------------
// <copyright file="ServiceBusClient.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus
{
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.ServiceBus.Interfaces;
    using MassTransit;

    /// <summary>
    /// The service bus client.
    /// </summary>
    public class ServiceBusClient : IServiceBusClient
    {
        /// <summary>
        /// The bus control.
        /// </summary>
        private readonly IBusControl _busControl;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBusClient"/> class.
        /// </summary>
        /// <param name="busControl">The bus control.</param>
        public ServiceBusClient(IBusControl busControl)
        {
            this._busControl = busControl;
        }

        /// <summary>
        /// Broadcasts the specified message.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        public async Task Broadcast<TMessage>(TMessage message, CancellationToken cancellationToken)
        {
            await this._busControl.Publish(message, cancellationToken);
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <typeparam name="TRequest">The type of the parameter.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="request">The parameter.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        public async Task<TResponse> Request<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
            where TRequest : class
            where TResponse : class
        {
            var response = await this._busControl.Request<TRequest, TResponse>(request, cancellationToken);
            return response.Message;
        }
    }
}