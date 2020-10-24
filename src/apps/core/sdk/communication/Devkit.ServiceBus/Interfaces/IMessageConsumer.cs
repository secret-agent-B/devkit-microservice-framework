// -----------------------------------------------------------------------
// <copyright file="IMessageConsumer.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus.Interfaces
{
    using MassTransit;

    /// <summary>
    /// The IMessageConsumer.
    /// </summary>
    public interface IMessageConsumer<TMessage> : IConsumer<TMessage>
        where TMessage : class, IMessage
    {
    }

    /// <summary>
    /// The IMessageConsumer.
    /// </summary>
    public interface IMessageConsumer<TMessage, TResponse> : IConsumer<TMessage>
        where TMessage : class, IMessage<TResponse>
    {
    }
}