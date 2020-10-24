// -----------------------------------------------------------------------
// <copyright file="FileVM.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.FileStore.Business.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Devkit.Communication.FileStore.DTOs;
    using Devkit.Patterns.CQRS;

    /// <summary>
    /// A FileVM is a representation of a file that can be uploaded or downloaded into the FileStore API.
    /// </summary>
    public class FileVM : ResponseBase, IFileDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileVM"/> class.
        /// </summary>
        public FileVM()
        {
            this.Contents = new List<byte>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileVM" /> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public FileVM(byte[] content)
        {
            this.Contents = content.ToList();
        }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public IList<byte> Contents { get; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public long FileSize { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }
    }
}