// -----------------------------------------------------------------------
// <copyright file="DownloadFileConsumer.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.FileStore.ServiceBus.Consumers
{
    using Devkit.Communication.FileStore.DTOs;
    using Devkit.ServiceBus.Interfaces;

    /// <summary>
    /// A request that is sent to the bus to download a file.
    /// </summary>
    public interface IDownloadFile : IMessage<IFileDTO>
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string Id { get; }
    }
}