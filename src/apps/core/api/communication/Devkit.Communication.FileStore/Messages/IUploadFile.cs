// -----------------------------------------------------------------------
// <copyright file="IStoreBytes.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.FileStore.Messages
{
    using System.Collections.Generic;
    using Devkit.Communication.FileStore.DTOs;
    using Devkit.ServiceBus.Interfaces;

    /// <summary>
    /// The IUploadFile is a message that is sent to the bus to store an array of bytes to the consuming API.
    /// </summary>
    public interface IUploadFile : IMessage<IFileDTO>
    {
        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        IList<byte> Contents { get; }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        string FileName { get; }
    }
}