// -----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Gateway
{
    using Devkit.Gateway.Extensions;
    using Devkit.WebAPI;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Ocelot.DependencyInjection;
    using Ocelot.Middleware;
    using Ocelot.Provider.Consul;
    using Ocelot.Provider.Polly;

    /// <summary>
    /// The startup class for the security API.
    /// </summary>
    public class Startup : AppStartupBase
    {
        /// <summary>
        /// Default Cors Policy name.
        /// </summary>
        private const string _defaultPolicy = "DefaultPolicy";

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">The env.</param>
        /// <param name="configuration">The configuration.</param>
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
            : base(env, configuration)
        {
        }

        /// <summary>
        /// Setup middleware.
        /// </summary>
        /// <param name="app">The application.</param>
        protected override void CustomConfigure(IApplicationBuilder app)
        {
            app.UseCors(_defaultPolicy);
            // app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseOcelot().Wait();
        }

        /// <summary>
        /// Configure middlewares.
        /// </summary>
        /// <param name="services">The services.</param>
        protected override void CustomConfigureServices(IServiceCollection services)
        {
            services
                .AddCors(options =>
                    options.AddPolicy(_defaultPolicy,
                        policy => policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                    )
                )
                .AddSecurity()
                .AddOcelot()
                .AddPolly()
                .AddConsul();
        }
    }
}