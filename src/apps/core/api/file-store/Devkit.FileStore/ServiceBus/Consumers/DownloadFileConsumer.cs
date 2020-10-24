// -----------------------------------------------------------------------
// <copyright file="DownloadFileConsumer.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.FileStore.ServiceBus.Consumers
{
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Devkit.Communication.FileStore.DTOs;
    using Devkit.Data.Interfaces;
    using Devkit.FileStore.Interfaces;
    using Devkit.ServiceBus;
    using MassTransit;

    /// <summary>
    /// The consumer for IDownloadFileRequest message.
    /// </summary>
    /// <seealso cref="IConsumer{DownloadFileRequest}" />
    public class DownloadFileConsumer : MessageConsumerBase<IDownloadFile, IFileDTO>
    {
        /// <summary>
        /// The file store repository.
        /// </summary>
        private readonly IFileStoreRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadFileConsumer" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public DownloadFileConsumer(IFileStoreRepository repository)
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
            var response = await this._repository.Download(context.Message.Id);
            await context.RespondAsync<IFileDTO>(response);
        }
    }
}