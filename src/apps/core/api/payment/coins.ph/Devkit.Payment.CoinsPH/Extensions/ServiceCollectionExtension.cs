// -----------------------------------------------------------------------
// <copyright file="ServiceCollectionExtension.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Payment.CoinsPH.Extensions
{
    using Devkit.Payment.CoinsPH;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The ServiceCollectionExtension provides extension methods for DI.
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Adds the coins ph.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddCoinsPH(this IServiceCollection services)
        {
            // Register the configuration.
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            services.Configure<CoinsPHConfiguration>(configuration.GetSection(CoinsPHConfiguration.ConfigSection));

            // Register the service
            services.AddTransient<CoinsPHService>();

            // Return the service collection back.
            return services;
        }
    }
}