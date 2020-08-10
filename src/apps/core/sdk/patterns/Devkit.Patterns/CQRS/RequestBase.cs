// -----------------------------------------------------------------------
// <copyright file="RequestBase.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.CQRS
{
    using MediatR;

    /// <summary>
    /// The input base class.
    /// </summary>
    /// <typeparam name="TResponse">The type of the output.</typeparam>
    public class RequestBase<TResponse> : IRequest<TResponse>
        where TResponse : ResponseBase
    {
    }
}