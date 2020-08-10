// -----------------------------------------------------------------------
// <copyright file="QueryHandlerBase.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.CQRS.Query
{
    using Devkit.Data.Interfaces;

    /// <summary>
    /// The query handler base class.
    /// </summary>
    /// <typeparam name="TRequest">The type of the input.</typeparam>
    /// <typeparam name="TResponse">The type of the output.</typeparam>
    /// <seealso cref="HandlerBase{TRequest, TResponse}" />
    public abstract class QueryHandlerBase<TRequest, TResponse> : HandlerBase<TRequest, TResponse>
        where TRequest : QueryRequestBase<TResponse>
        where TResponse : ResponseBase, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryHandlerBase{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        protected QueryHandlerBase(IRepository repository)
            : base(repository)
        {
        }
    }
}