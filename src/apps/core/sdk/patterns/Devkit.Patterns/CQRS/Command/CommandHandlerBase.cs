// -----------------------------------------------------------------------
// <copyright file="CommandHandlerBase.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.CQRS.Command
{
    using Devkit.Data.Interfaces;

    /// <summary>
    /// The command handler base class.
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <typeparam name="TOutput">The type of the output.</typeparam>
    public abstract class CommandHandlerBase<TInput, TOutput> : HandlerBase<TInput, TOutput>
        where TInput : CommandRequestBase<TOutput>
        where TOutput : ResponseBase, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandHandlerBase{TInput, TOutput}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        protected CommandHandlerBase(IRepository repository)
            : base(repository)
        {
        }
    }
}