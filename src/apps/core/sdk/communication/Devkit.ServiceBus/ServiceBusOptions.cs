// -----------------------------------------------------------------------
// <copyright file="ServiceBusOptions.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus
{
    /// <summary>
    /// The service bus options class.
    /// </summary>
    public class ServiceBusOptions
    {
        /// <summary>
        /// The configuration section.
        /// </summary>
        public const string Section = "ServiceBusOptions";

        /// <summary>
        /// Gets or sets the heartbeat in seconds.
        /// </summary>
        /// <value>
        /// The heartbeat in seconds.
        /// </value>
        public ushort Heartbeat { get; set; }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string Username { get; set; }
    }
}