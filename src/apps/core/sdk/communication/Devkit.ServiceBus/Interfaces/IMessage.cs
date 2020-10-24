// -----------------------------------------------------------------------
// <copyright file="IMessage.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus.Interfaces
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The IMessage is the marker interface for messages.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    [SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Marker interface.")]
    public interface IMessage<TResponse> : IMessage
    {
    }

    /// <summary>
    /// The IMessage is the marker interface for messages.
    /// </summary>
    [SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Marker interface.")]
    public interface IMessage
    {
    }
}