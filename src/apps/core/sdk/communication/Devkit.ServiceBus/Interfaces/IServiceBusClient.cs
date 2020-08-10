// -----------------------------------------------------------------------
// <copyright file="IServiceBusClient.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The service bus client interface used by the application to send messages outside of the API.
    /// </summary>
    public interface IServiceBusClient
    {
        /// <summary>
        /// Broadcasts the specified message.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        Task Broadcast<TMessage>(TMessage message, CancellationToken cancellationToken);

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        Task<TResponse> Request<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
            where TRequest : class
            where TResponse : class;
    }
}