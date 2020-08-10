// -----------------------------------------------------------------------
// <copyright file="ResponseBehavior.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.CQRS.Behaviors
{
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Patterns.CQRS.Contracts;
    using Devkit.Patterns.Exceptions;
    using MediatR.Pipeline;

    /// <summary>
    /// The behavior that checks if the request was successful, if not - throws an exception.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <seealso cref="IRequestPostProcessor{TRequest, TResponse}" />
    public class ResponseBehavior<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
        where TResponse : IResponse
    {
        /// <summary>
        /// Process method executes after the Handle method on your handler.
        /// </summary>
        /// <param name="request">Request instance.</param>
        /// <param name="response">Response instance.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>
        /// An awaitable task.
        /// </returns>
        public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            if (response.IsSuccessful)
            {
                return Task.CompletedTask;
            }

            var requestException = new RequestException();

            foreach (var item in response.Exceptions)
            {
                requestException.AddRange(item.Key, item.Value);
            }

            throw requestException;
        }
    }
}