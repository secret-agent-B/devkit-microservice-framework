// -----------------------------------------------------------------------
// <copyright file="IDeleteFile.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.FileStore.Messages
{
    /// <summary>
    /// The IDeleteFile is the message for deleting a file.
    /// </summary>
    public interface IDeleteFile
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