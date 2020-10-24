// -----------------------------------------------------------------------
// <copyright file="IConsumerException.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus.Exceptions
{
    /// <summary>
    /// The IConsumerException is the exception that is thrown when an unhandled consumer exception occured.
    /// </summary>
    public interface IConsumerException
    {
        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        string ErrorMessage { get; }
    }
}