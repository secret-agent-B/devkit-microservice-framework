// -----------------------------------------------------------------------
// <copyright file="ConsulServiceConfiguration.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.WebAPI.ServiceRegistry
{
    using System.Collections.Generic;

    /// <summary>
    /// Consul service configuration used for microservice registration into Consul.
    /// </summary>
    public class ConsulServiceConfiguration
    {
        /// <summary>
        /// The consul configuration.
        /// </summary>
        internal const string _section = "ConsulConfiguration";

        /// <summary>
        /// Gets or sets the consul host.
        /// </summary>
        /// <value>
        /// The consul host.
        /// </value>
        public string ConsulHost { get; set; }

        /// <summary>
        /// Gets or sets the data center.
        /// </summary>
        /// <value>
        /// The data center.
        /// </value>
        public string DataCenter { get; set; }

        /// <summary>
        /// Gets or sets the gateway route.
        /// </summary>
        /// <value>
        /// The gateway route.
        /// </value>
        public string Gateway { get; set; }

        /// <summary>
        /// Gets or sets the live check in seconds.
        /// </summary>
        /// <value>
        /// The check in seconds.
        /// </value>
        public int LiveCheckInSeconds { get; set; } = 30;

        /// <summary>
        /// Gets the meta.
        /// </summary>
        /// <value>
        /// The meta.
        /// </value>
        public Dictionary<string, string> Meta { get; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the service address.
        /// </summary>
        /// <value>
        /// The service address.
        /// </value>
        public string ServiceAddress { get; set; }

        /// <summary>
        /// Gets or sets the service id.
        /// </summary>
        /// <value>
        /// The service id.
        /// </value>
        public string ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the service names.
        /// </summary>
        /// <value>
        /// The service names.
        /// </value>
        public string ServiceName { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public IEnumerable<string> Tags { get; set; }
    }
}