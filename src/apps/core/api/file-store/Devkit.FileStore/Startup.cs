// -----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.FileStore
{
    using Devkit.FileStore.Data;
    using Devkit.FileStore.Interfaces;
    using Devkit.FileStore.ServiceBus;
    using Devkit.ServiceBus.Extensions;
    using Devkit.WebAPI;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The Startup class for the FileStore API that sets up the services and DI.
    /// </summary>
    /// <seealso cref="AppStartupBase" />
    public class Startup : AppStartupBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">The env.</param>
        /// <param name="configuration">The configuration.</param>
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
            : base(env, configuration)
        {
            this.MediatorAssemblies.Add(typeof(Startup).Assembly);
            this.ValidationAssemblies.Add(typeof(Startup).Assembly);
        }

        /// <summary>
        /// Setup middleware.
        /// </summary>
        /// <param name="app">The application.</param>
        protected override void CustomConfigure(IApplicationBuilder app)
        {
        }

        /// <summary>
        /// Configure middlewares.
        /// </summary>
        /// <param name="services">The services.</param>
        protected override void CustomConfigureServices(IServiceCollection services)
        {
            services.AddServiceBus<FileStoreRegistry>();
            services.AddTransient<IFileStoreRepository, FileStoreRepository>();
        }
    }
}