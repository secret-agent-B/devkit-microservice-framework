// -----------------------------------------------------------------------
// <copyright file="UploadFileCommand.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.FileStore.Business.Files.Commands.UploadFile
{
    using System.Diagnostics.CodeAnalysis;
    using Devkit.FileStore.Business.ViewModels;
    using Devkit.Patterns.CQRS.Command;

    /// <summary>
    /// The request for saving a file into the database.
    /// </summary>
    public class UploadFileCommand : CommandRequestBase<FileVM>
    {
        /// <summary>
        /// Gets or sets the contents.
        /// </summary>
        /// <value>
        /// The contents.
        /// </value>
        [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Using array to avoid conversion back and forth.")]
        public byte[] Contents { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }
    }
}