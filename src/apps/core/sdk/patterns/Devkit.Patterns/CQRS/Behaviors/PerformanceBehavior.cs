// -----------------------------------------------------------------------
// <copyright file="PerformanceBehavior.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.CQRS.Behaviors
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Patterns.Properties;
    using MediatR;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Logs low performing handlers to notify the development team of possible enhancements.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <seealso cref="IPipelineBehavior{TRequest, TResponse}" />
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<TRequest> _logger;

        /// <summary>
        /// The timer.
        /// </summary>
        private readonly Stopwatch _timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformanceBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public PerformanceBehavior(ILogger<TRequest> logger)
        {
            this._timer = new Stopwatch();
            this._logger = logger;
        }

        /// <summary>
        /// Pipeline handler. Perform any additional behavior and await the <paramref name="next" /> delegate as necessary.
        /// </summary>
        /// <param name="request">Incoming request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="next">Awaitable delegate for the next action in the pipeline. Eventually this delegate represents the handler.</param>
        /// <returns>
        /// Awaitable task returning the <typeparamref name="TResponse" />.
        /// </returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            this._timer.Start();

            var response = await next();

            this._timer.Stop();

            if (this._timer.ElapsedMilliseconds <= 500)
            {
                return response;
            }

            this._logger.LogWarning(Resources.PERFORMANCE_BEHAVIOR_WARN_MESSAGE, typeof(TRequest).Name, this._timer.ElapsedMilliseconds, request);

            return response;
        }
    }
}