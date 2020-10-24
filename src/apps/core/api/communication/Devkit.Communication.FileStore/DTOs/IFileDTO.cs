// -----------------------------------------------------------------------
// <copyright file="IFileDTO.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.FileStore.DTOs
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The IFileDTO is a service bus response for downloading a file.
    /// </summary>
    public interface IFileDTO
    {
        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        IList<byte> Contents { get; }

        /// <summary>
        /// Gets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        DateTime CreatedOn { get; }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        string FileName { get; }

        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        long FileSize { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string Id { get; }
    }
}