// -----------------------------------------------------------------------
// <copyright file="UploadBase64ImageConsumer.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.FileStore.ServiceBus.Consumers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Devkit.Communication.FileStore.DTOs;
    using Devkit.Communication.FileStore.Extensions;
    using Devkit.Communication.FileStore.Messages;
    using Devkit.FileStore.Interfaces;
    using Devkit.ServiceBus;
    using Devkit.ServiceBus.Exceptions;
    using MassTransit;

    /// <summary>
    /// The UploadBase64ImageConsumer is the consumer for IUploadBase64ImageConsumer.
    /// </summary>
    public class UploadBase64ImageConsumer : MessageConsumerBase<IUploadBase64Image, IFileDTO>
    {
        /// <summary>
        /// The file store repository.
        /// </summary>
        private readonly IFileStoreRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadBase64ImageConsumer" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UploadBase64ImageConsumer(IFileStoreRepository repository)
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
        protected override async Task ConsumeMessage(ConsumeContext<IUploadBase64Image> context)
        {
            var contents = context.Message.Contents.ToImage(context.Message.Size);
            var response = await this._repository.Upload(context.Message.FileName, contents);

            if (response.IsSuccessful)
            {
                await context.RespondAsync<IFileDTO>(response);
            }
            else
            {
                await context.RespondAsync<IConsumerException>(new
                {
                    ErrorMessage = string.Join(", ", response.Exceptions.SelectMany(x => x.Value))
                });
            }
        }
    }
}