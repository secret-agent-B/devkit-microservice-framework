// -----------------------------------------------------------------------
// <copyright file="IFileStoreRepository.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.FileStore.Interfaces
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.FileStore.Business.ViewModels;

    /// <summary>
    /// The file store repository.
    /// </summary>
    /// <seealso cref="IRepository" />
    public interface IFileStoreRepository : IRepository
    {
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Delete(string id);

        /// <summary>
        /// Downloads the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The uploaded object.</returns>
        Task<FileVM> Download(string id);

        /// <summary>
        /// Uploads the specified stream.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="content">The content.</param>
        /// <returns>
        /// A string that represents the id of the uploaded file.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        Task<FileVM> Upload(string fileName, IList<byte> content);

        /// <summary>
        /// Uploads the specified content.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="content">The content.</param>
        /// <returns>
        /// A string that represents the id of the uploaded file.
        /// </returns>
        Task<FileVM> Upload(string fileName, string content);
    }
}