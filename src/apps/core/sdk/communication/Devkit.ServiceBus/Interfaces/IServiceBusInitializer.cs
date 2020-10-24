// -----------------------------------------------------------------------
// <copyright file="IServiceBusInitializer.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus.Interfaces
{
    /// <summary>
    /// The IServiceBusInitializer is the contract for service bus initializer.
    /// </summary>
    public interface IServiceBusInitializer
    {
        /// <summary>
        /// Connects this instance.
        /// </summary>
        /// <returns>
        /// A service collection.
        /// </returns>
        void Initialize();
    }
}