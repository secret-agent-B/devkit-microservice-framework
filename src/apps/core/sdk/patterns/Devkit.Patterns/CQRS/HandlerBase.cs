// -----------------------------------------------------------------------
// <copyright file="HandlerBase.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.CQRS
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using MediatR;

    /// <summary>
    /// The handler base class.
    /// </summary>
    /// <typeparam name="TRequest">The type of the input.</typeparam>
    /// <typeparam name="TResponse">The type of the output.</typeparam>
    /// <seealso cref="IRequestHandler{TRequest, TResponse}" />
    /// <seealso cref="IDisposable" />
    public abstract class HandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>, IDisposable
        where TRequest : RequestBase<TResponse>
        where TResponse : ResponseBase, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandlerBase{TInput, TOutput}" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        protected HandlerBase(IRepository repository)
        {
            this.Repository = repository;
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        protected IRepository Repository { get; }

        /// <summary>
        /// Gets the input.
        /// </summary>
        /// <value>
        /// The input.
        /// </value>
        protected TRequest Request { get; private set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        protected TResponse Response { get; set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>
        /// Response from the request.
        /// </returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            this.Request = request;
            this.Response = Activator.CreateInstance<TResponse>();

            try
            {
                await this.ExecuteAsync(cancellationToken);
            }
            catch
            {
                await this.RevertOperation(cancellationToken);
                throw;
            }
            finally
            {
                this.Dispose();
            }

            return this.Response;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task.</returns>
        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Reverts the operation.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task.</returns>
        protected virtual Task RevertOperation(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}