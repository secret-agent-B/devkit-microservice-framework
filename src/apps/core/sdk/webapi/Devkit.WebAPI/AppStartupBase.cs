// -----------------------------------------------------------------------
// <copyright file="AppStartupBase.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.WebAPI
{
    using System.Collections.Generic;
    using System.Reflection;
    using Devkit.Data.Extensions;
    using Devkit.Patterns.CQRS.Extensions;
    using Devkit.ServiceBus.Extensions;
    using Devkit.WebAPI.Extensions;
    using Devkit.WebAPI.Filters;
    using Devkit.WebAPI.ServiceRegistry;
    using MassTransit.ExtensionsDependencyInjectionIntegration;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// The application startup base.
    /// </summary>
    public abstract class AppStartupBase
    {
        /// <summary>
        /// The API definition.
        /// </summary>
        private readonly APIDefinition _apiDefinition;

        /// <summary>
        /// The test environment.
        /// </summary>
        private readonly bool _isUnitTestEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppStartupBase" /> class.
        /// </summary>
        /// <param name="env">The env.</param>
        /// <param name="configuration">The configuration.</param>
        protected AppStartupBase(IWebHostEnvironment env, IConfiguration configuration)
        {
            this.WebHostEnvironment = env;
            this.Configuration = configuration;

            this.MediatorAssemblies = new HashSet<Assembly>();
            this.ValidationAssemblies = new HashSet<Assembly>();

            this._apiDefinition = configuration.GetAPIDefinition();
            this._isUnitTestEnvironment = this.WebHostEnvironment.IsEnvironment("unit-test");
        }

        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        /// <value>
        /// IConfigurationRoot.
        /// </value>
        protected IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the application mediator assemblies.
        /// </summary>
        /// <value>
        /// MediatorAssemblies.
        /// </value>
        protected ICollection<Assembly> MediatorAssemblies { get; }

        /// <summary>
        /// Gets the application validation assemblies.
        /// </summary>
        /// <value>
        /// ValidationAssemblies.
        /// </value>
        protected ICollection<Assembly> ValidationAssemblies { get; }

        /// <summary>
        /// Gets the web host environment.
        /// </summary>
        /// <value>
        /// The web host environment.
        /// </value>
        protected IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configure(IApplicationBuilder app)
        {
            if (this.WebHostEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseHealthChecks("/health");
            app.UseSwagger(this._apiDefinition);

            this.CustomConfigure(app);

            // UseRouting adds route matching to the middleware pipeline.
            // This middleware looks at the set of endpoints defined in the app,
            // and selects the best match based on the request.
            app.UseRouting();

            // UseEndpoints adds endpoint execution to the middleware pipeline. It runs the delegate associated with the selected endpoint.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var mvcBuilder = services
                .AddMvc(options =>
                {
                    options.Filters.Add(typeof(PipelineFilterAttribute));
                });

            services.AddHealthChecks();
            services.AddRepository();
            services.AddMediatRAssemblies(this.MediatorAssemblies);

            services.AddServiceRegistry();
            services.AddServiceBus(this.AddConsumers, this._isUnitTestEnvironment);
            services.AddSwagger(this._apiDefinition);

            mvcBuilder.AddValidationAssemblies(this.ValidationAssemblies);

            this.CustomConfigureServices(services);
        }

        /// <summary>
        /// Adds the consumers.
        /// </summary>
        /// <param name="configurator">The configurator.</param>
        protected virtual void AddConsumers(IServiceCollectionBusConfigurator configurator)
        {
            // do nothing...
        }

        /// <summary>
        /// Setup middleware.
        /// </summary>
        /// <param name="app">The application.</param>
        protected abstract void CustomConfigure(IApplicationBuilder app);

        /// <summary>
        /// Configure middlewares.
        /// </summary>
        /// <param name="services">The services.</param>
        protected abstract void CustomConfigureServices(IServiceCollection services);
    }
}