// -----------------------------------------------------------------------
// <copyright file="FileStoreRepository.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.FileStore.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Devkit.Data;
    using Devkit.FileStore.Business.ViewModels;
    using Devkit.FileStore.Interfaces;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.GridFS;

    /// <summary>
    /// The file store repository.
    /// </summary>
    /// <seealso cref="Repository" />
    /// <seealso cref="IFileStoreRepository" />
    public class FileStoreRepository : Repository, IFileStoreRepository
    {
        /// <summary>
        /// The database.
        /// </summary>
        private readonly IMongoDatabase _database;

        /// <summary>
        /// The grid fs.
        /// </summary>
        private readonly GridFSBucket _gridFs;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileStoreRepository"/> class.
        /// </summary>
        /// <param name="optionsAccessor">The options accessor.</param>
        public FileStoreRepository(RepositoryOptions optionsAccessor)
            : base(optionsAccessor)
        {
            var client = new MongoClient(optionsAccessor?.ConnectionString);
            this._database = client.GetDatabase(optionsAccessor.DatabaseName);
            this._gridFs = new GridFSBucket(this._database);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Delete(string id)
        {
            await this._gridFs.DeleteAsync(new ObjectId(id));
        }

        /// <summary>
        /// Downloads the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The downloaded file.
        /// </returns>
        public async Task<FileVM> Download(string id)
        {
            var objectId = new ObjectId(id);
            var stream = await this._gridFs.OpenDownloadStreamAsync(objectId);

            if (stream == null)
            {
                return null;
            }

            var fileInfo = stream.FileInfo;
            var contents = await this._gridFs.DownloadAsBytesAsync(objectId);

            return new FileVM(contents)
            {
                CreatedOn = fileInfo.UploadDateTime,
                FileName = fileInfo.Filename,
                FileSize = fileInfo.Length,
                Id = fileInfo.Id.ToString()
            };
        }

        /// <summary>
        /// Uploads the specified stream.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="content">The content.</param>
        /// <returns>
        /// The uploaded file.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<FileVM> Upload(string fileName, IList<byte> content)
        {
            var objectId = await this._gridFs.UploadFromBytesAsync(fileName, content.ToArray());
            var stream = await this._gridFs.OpenDownloadStreamAsync(objectId);

            if (stream == null)
            {
                return null;
            }

            var fileInfo = stream.FileInfo;

            return new FileVM()
            {
                Id = fileInfo.Id.ToString(),
                FileName = fileInfo.Filename,
                CreatedOn = fileInfo.UploadDateTime,
                FileSize = fileInfo.Length
            };
        }

        /// <summary>
        /// Uploads the specified content.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="content">The content.</param>
        /// <returns>
        /// The uploaded file.
        /// </returns>
        public async Task<FileVM> Upload(string fileName, string content)
        {
            var stringBytes = Encoding.UTF8.GetBytes(content);
            return await this.Upload(fileName, stringBytes);
        }
    }
}