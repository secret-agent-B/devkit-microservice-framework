// -----------------------------------------------------------------------
// <copyright file="DeleteFileConsumer.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.FileStore.ServiceBus.Consumers
{
    using System;
    using System.Threading.Tasks;
    using Devkit.Communication.FileStore.DTOs;
    using Devkit.FileStore.Interfaces;
    using Devkit.ServiceBus;
    using Devkit.ServiceBus.Exceptions;
    using MassTransit;

    /// <summary>
    /// The DeleteFileConsumer is the consumer for IDeleteFile message.
    /// </summary>
    /// <seealso cref="MessageConsumerBase{IDownloadFile, IFileDTO}" />
    public class DeleteFileConsumer : MessageConsumerBase<IDownloadFile, IFileDTO>
    {
        /// <summary>
        /// The file repository.
        /// </summary>
        private readonly IFileStoreRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteFileConsumer"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public DeleteFileConsumer(IFileStoreRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected override async Task ConsumeMessage(ConsumeContext<IDownloadFile> context)
        {
            try
            {
                await this._repository.Delete(context.Message.Id);
            }
            catch (Exception ex)
            {
                await context.RespondAsync<IConsumerException>(new
                {
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}