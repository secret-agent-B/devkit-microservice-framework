// -----------------------------------------------------------------------
// <copyright file="RequestValidationBehavior.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.CQRS.Behaviors
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Patterns.Exceptions;
    using FluentValidation;
    using MediatR;
    using Serilog;

    /// <summary>
    /// The pipeline behavior that validates incoming requests before it gets processed by the handler.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <seealso cref="IPipelineBehavior{TRequest, TResponse}" />
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// The validators.
        /// </summary>
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestValidationBehavior{TRequest, TResponse}" /> class.
        /// </summary>
        /// <param name="validators">The validators.</param>
        /// <param name="logger">The logger.</param>
        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger logger)
        {
            this._validators = validators;
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
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Handled by MediatR.")]
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = this._validators.Where(v => v.CanValidateInstancesOfType(typeof(TRequest)))
                .Select(v => v.Validate(context))
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .Distinct()
                .ToList();

            if (failures.Any())
            {
                var requestName = typeof(TRequest).Name;
                var behaviorName = typeof(RequestValidationBehavior<TRequest, TResponse>).Name;

                this._logger
                    .ForContext("Behavior", behaviorName)
                    .ForContext("RequestName", requestName)
                    .ForContext("ValidRequest", false)
                    .ForContext("RequestPayload", request, true)
                    .ForContext("Errors", failures, true)
                    .Information("Validated request with errors.");

                throw new AppException(failures);
            }

            return next();
        }
    }
}