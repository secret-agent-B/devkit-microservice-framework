// -----------------------------------------------------------------------
// <copyright file="DownloadFileHandler.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.FileStore.Business.Files.Queries.DownloadFile
{
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.FileStore.Business.ViewModels;
    using Devkit.FileStore.Interfaces;
    using Devkit.Patterns.CQRS.Query;
    using Devkit.Patterns.Exceptions;

    /// <summary>
    /// The handler for DownloadFileQuery.
    /// </summary>
    public class DownloadFileHandler : QueryHandlerBase<DownloadFileQuery, FileVM>
    {
        /// <summary>
        /// The file store repository.
        /// </summary>
        private readonly IFileStoreRepository _fileStoreRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadFileHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public DownloadFileHandler(IFileStoreRepository repository)
            : base(repository)
        {
            this._fileStoreRepository = repository;
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            this.Response = await this._fileStoreRepository.Download(this.Request.Id);

            if (this.Response == null)
            {
                throw new NotFoundException($"File ({this.Request.Id}) not found.");
            }
        }
    }
}