// -----------------------------------------------------------------------
// <copyright file="DownloadFileQuery.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.FileStore.Business.Files.Queries.DownloadFile
{
    using Devkit.FileStore.Business.ViewModels;
    using Devkit.Patterns.CQRS.Query;

    /// <summary>
    /// The query that is sent to the file-store API for downloading a file.
    /// </summary>
    /// <seealso cref="QueryRequestBase{FileVM}" />
    public class DownloadFileQuery : QueryRequestBase<FileVM>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }
    }
}