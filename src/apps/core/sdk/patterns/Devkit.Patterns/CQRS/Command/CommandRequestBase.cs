// -----------------------------------------------------------------------
// <copyright file="CommandRequestBase.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.CQRS.Command
{
    /// <summary>
    /// The command input base class.
    /// </summary>
    /// <typeparam name="TOutput">The type of the output.</typeparam>
    /// <seealso cref="RequestBase{TOutput}" />
    public class CommandRequestBase<TOutput> : RequestBase<TOutput>
        where TOutput : ResponseBase, new()
    {
    }
}