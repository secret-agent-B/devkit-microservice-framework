// -----------------------------------------------------------------------
// <copyright file="UploadFileHandler.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.FileStore.Business.Files.Commands.UploadFile
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.FileStore.Business.ViewModels;
    using Devkit.FileStore.Interfaces;
    using Devkit.Patterns.CQRS.Command;

    /// <summary>
    /// The request handler for UploadFileCommand.
    /// </summary>
    public class UploadFileHandler : CommandHandlerBase<UploadFileCommand, FileVM>
    {
        /// <summary>
        /// The file store repository.
        /// </summary>
        private readonly IFileStoreRepository _fileStoreRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadFileHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UploadFileHandler(IFileStoreRepository repository)
                    : base(repository)
        {
            this._fileStoreRepository = repository;
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            this.Response = await this._fileStoreRepository.Upload(this.Request.FileName, this.Request.Contents);
        }
    }
}