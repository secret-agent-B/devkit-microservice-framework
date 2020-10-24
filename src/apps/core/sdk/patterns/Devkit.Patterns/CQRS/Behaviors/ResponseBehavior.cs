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
    using Serilog;

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
        /// The logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public ResponseBehavior(ILogger logger)
        {
            this._logger = logger;
        }

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
            var behaviorName = typeof(ResponseBehavior<TRequest, TResponse>).Name;
            var requestName = typeof(TRequest).Name;

            this._logger
                .ForContext("Behavior", behaviorName)
                .ForContext("IsSuccessful", response.IsSuccessful)
                .ForContext("RequestName", requestName)
                .ForContext("RequestPayload", request, true)
                .ForContext("ResponsePayload", response, true)
                .Information("Processed request.");

            if (response.IsSuccessful)
            {
                return Task.CompletedTask;
            }

            var requestException = new AppException();

            foreach (var item in response.Exceptions)
            {
                requestException.AddRange(item.Key, item.Value);
            }

            throw requestException;
        }
    }
}