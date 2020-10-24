// -----------------------------------------------------------------------
// <copyright file="UploadFileConsumer.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.FileStore.ServiceBus.Consumers
{
    using System.Threading.Tasks;
    using Devkit.Communication.FileStore.DTOs;
    using Devkit.Communication.FileStore.Messages;
    using Devkit.FileStore.Interfaces;
    using Devkit.ServiceBus;
    using MassTransit;

    /// <summary>
    /// The UploadFileConsumer consumes the IUploadFile message.
    /// </summary>
    /// <seealso cref="IConsumer{IUploadFile}" />
    public class UploadFileConsumer : MessageConsumerBase<IUploadFile, IFileDTO>
    {
        /// <summary>
        /// The file store repository.
        /// </summary>
        private readonly IFileStoreRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadFileConsumer" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UploadFileConsumer(IFileStoreRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override async Task ConsumeMessage(ConsumeContext<IUploadFile> context)
        {
            var response = await this._repository.Upload(context.Message.FileName, context.Message.Contents);
            await context.RespondAsync<IFileDTO>(response);
        }
    }
}