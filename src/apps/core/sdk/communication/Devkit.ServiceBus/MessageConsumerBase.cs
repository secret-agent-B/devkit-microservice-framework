// -----------------------------------------------------------------------
// <copyright file="MessageConsumerBase.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Devkit.ServiceBus.Exceptions;
    using Devkit.ServiceBus.Interfaces;
    using MassTransit;
    using Serilog;

    /// <summary>
    /// The MessageConsumerBase the base class for message consumers.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <seealso cref="IMessageConsumer{TMessage}" />
    public abstract class MessageConsumerBase<TMessage, TResponse> : IMessageConsumer<TMessage>
        where TMessage : class, IMessage<TResponse>
    {
        /// <summary>
        /// Consumes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// A task.
        /// </returns>
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Parameter provided by MassTransit.")]
        public async Task Consume(ConsumeContext<TMessage> context)
        {
            try
            {
                await this.ConsumeMessage(context);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);

                await context.RespondAsync<IConsumerException>(new
                {
                    ErrorMessage = ex.Message
                });
            }
        }

        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected abstract Task ConsumeMessage(ConsumeContext<TMessage> context);
    }
}