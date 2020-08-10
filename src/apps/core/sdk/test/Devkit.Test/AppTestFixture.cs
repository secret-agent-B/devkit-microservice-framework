// -----------------------------------------------------------------------
// <copyright file="AppTestFixture.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Test
{
    using System;
    using Devkit.Data;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Mongo2Go;

    /// <summary>
    /// Application test fixture to generate the test host.
    /// </summary>
    /// <typeparam name="TStartup">The type of the startup.</typeparam>
    /// <seealso cref="WebApplicationFactory{TStartup}" />
    public class AppTestFixture<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        /// <summary>
        /// The runner.
        /// </summary>
        private readonly MongoDbRunner _runner;

        /// <summary>
        /// The configuration.
        /// </summary>
        private Action<IServiceCollection> _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppTestFixture{TStartup}"/> class.
        /// </summary>
        public AppTestFixture()
        {
            this._runner = MongoDbRunner.Start();

            this.RepositoryConfiguration = new RepositoryOptions
            {
                ConnectionString = this._runner.ConnectionString,
                DatabaseName = Guid.NewGuid().ToString("N")
            };
        }

        /// <summary>
        /// Gets the repository configuration.
        /// </summary>
        /// <value>
        /// The repository configuration.
        /// </value>
        public RepositoryOptions RepositoryConfiguration { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public void ConfigureTestServices(Action<IServiceCollection> configuration)
        {
            this._configuration = configuration;
        }

        /// <summary>
        /// Gives a fixture an opportunity to configure the application before it gets built.
        /// </summary>
        /// <param name="builder">The <see cref="IWebHostBuilder" /> for the application.</param>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureTestServices(services =>
                {
                    this._configuration?.Invoke(services);
                });
        }

        /// <summary>
        /// Creates a <see cref="IWebHostBuilder" /> used to set up <see cref="TestServer" />.
        /// </summary>
        /// <returns>
        /// A <see cref="IWebHostBuilder" /> instance.
        /// </returns>
        /// <remarks>
        /// The default implementation of this method looks for a <c>public static IWebHostBuilder CreateWebHostBuilder(string[] args)</c>
        /// method defined on the entry point of the assembly of TEntryPoint and invokes it passing an empty string
        /// array as arguments.
        /// </remarks>
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");

            Environment.SetEnvironmentVariable("DISABLE_SERVICE_BUS", "true");
            Environment.SetEnvironmentVariable("DISABLE_SERVICE_REGISTRY", "true");
            Environment.SetEnvironmentVariable("DISABLE_SERVICE_SWAGGER", "true");

            return base.CreateWebHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton(this.RepositoryConfiguration);
                });
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing"><see langword="true" /> to release both managed and unmanaged resources;
        /// <see langword="false" /> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            this._runner.Dispose();
        }
    }
}