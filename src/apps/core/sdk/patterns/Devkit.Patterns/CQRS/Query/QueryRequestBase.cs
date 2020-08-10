// -----------------------------------------------------------------------
// <copyright file="QueryRequestBase.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.CQRS.Query
{
    /// <summary>
    /// The query input base class.
    /// </summary>
    /// <typeparam name="TResponse">The type of the output.</typeparam>
    /// <seealso cref="RequestBase{TOutput}" />
    public class QueryRequestBase<TResponse> : RequestBase<TResponse>
        where TResponse : ResponseBase, new()
    {
    }
}