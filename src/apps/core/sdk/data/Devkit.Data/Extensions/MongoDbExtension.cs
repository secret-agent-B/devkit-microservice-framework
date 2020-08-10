// -----------------------------------------------------------------------
// <copyright file="MongoDbExtension.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Data.Extensions
{
    using Devkit.Data.Interfaces;
    using Devkit.Data.Seeding;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The MongoDb extension class.
    /// </summary>
    public static class MongoDbExtension
    {
        /// <summary>
        /// Adds the mongo repository.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>
        /// The service collection.
        /// </returns>
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var repoOptions = provider.GetService<RepositoryOptions>();

            if (repoOptions == null)
            {
                repoOptions = provider
                    .GetRequiredService<IConfiguration>()
                    .GetSection(RepositoryOptions._section)
                    .Get<RepositoryOptions>();
            }

            if (repoOptions != null)
            {
                services.AddSingleton(repoOptions);
                services.AddTransient<IRepository, Repository>();
            }

            return services;
        }

        /// <summary>
        /// Executes the seeder.
        /// </summary>
        /// <typeparam name="TSeeder">The type of the seeder.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        /// The service collection.
        /// </returns>
        public static IServiceCollection AddSeedData<TSeeder>(this IServiceCollection services, string filePath = "")
            where TSeeder : class, ISeeder
        {
            services.AddTransient<ISeederConfig, SeederConfig>(x => new SeederConfig(filePath));
            services.AddTransient<TSeeder>();

            var provider = services.BuildServiceProvider();
            var seeder = provider.GetService<TSeeder>();

            seeder.Execute();

            return services;
        }
    }
}