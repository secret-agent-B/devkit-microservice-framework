// -----------------------------------------------------------------------
// <copyright file="EndpointNameFormatter.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus
{
    using System;
    using MassTransit;
    using MassTransit.Definition;

    /// <summary>
    /// An endpoint name formatter.
    /// </summary>
    /// <seealso cref="DefaultEndpointNameFormatter" />
    /// <seealso cref="IEndpointNameFormatter" />
    internal class EndpointNameFormatter : DefaultEndpointNameFormatter, IEndpointNameFormatter
    {
        /// <summary>
        /// Consumers this instance.
        /// </summary>
        /// <typeparam name="T">The type of consumer.</typeparam>
        /// <returns>An endpoint name.</returns>
        string IEndpointNameFormatter.Consumer<T>()
        {
            const string consumer = "Consumer";

            var consumerName = $"{consumer}_{typeof(T).FullName}";

            if (string.IsNullOrEmpty(consumerName))
            {
                consumerName = $"{consumer}_{typeof(T).Name}";
            }

            if (consumerName.EndsWith(consumer, StringComparison.InvariantCultureIgnoreCase))
            {
                consumerName = consumerName.Substring(0, consumerName.Length - consumer.Length);
            }

            return this.SanitizeName(consumerName);
        }
    }
}