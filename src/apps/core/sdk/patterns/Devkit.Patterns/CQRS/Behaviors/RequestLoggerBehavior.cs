// -----------------------------------------------------------------------
// <copyright file="RequestLoggerBehavior.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.CQRS.Behaviors
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR.Pipeline;
    using Serilog;

    /// <summary>
    /// This behavior logs all incoming requests prior to execution.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <seealso cref="IRequestPreProcessor{TRequest}" />
    public class RequestLoggerBehavior<TRequest> : IRequestPreProcessor<TRequest>
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestLoggerBehavior{TRequest}"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public RequestLoggerBehavior(ILogger logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// Process method executes before calling the Handle method on your handler.
        /// </summary>
        /// <param name="request">Incoming request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>
        /// An awaitable task.
        /// </returns>
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var behaviorName = typeof(RequestLoggerBehavior<TRequest>).Name;
            var requestName = typeof(TRequest).Name;

            this._logger
                .ForContext("Behavior", behaviorName)
                .ForContext("RequestName", requestName)
                .ForContext("RequestPayload", request, true)
                .Information("Processing CQRS request.");

            return Task.CompletedTask;
        }
    }
}