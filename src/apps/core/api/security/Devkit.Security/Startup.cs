// -----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security
{
    using System;
    using AspNetCore.Identity.Mongo;
    using Devkit.Data;
    using Devkit.Data.Extensions;
    using Devkit.Security.Data;
    using Devkit.Security.Data.Models;
    using Devkit.Security.Extensions;
    using Devkit.WebAPI;
    using IdentityServer4;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MongoDB.Bson.Serialization.Conventions;

    /// <summary>
    /// The Startup class for the Security API.
    /// </summary>
    public class Startup : AppStartupBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="webHostEnvironment">The web host environment.</param>
        /// <param name="configuration">The configuration.</param>
        public Startup(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
            : base(webHostEnvironment, configuration)
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
            app.UseIdentityServer();
            app.UseAuthorization();
        }

        /// <summary>
        /// Configure middlewares.
        /// </summary>
        /// <param name="services">The services.</param>
        protected override void CustomConfigureServices(IServiceCollection services)
        {
            ConventionRegistry
                .Register(
                    "Ignore extra elements",
                    new ConventionPack
                    {
                        new IgnoreExtraElementsConvention(true),
                    },
                    type => true);

            services
                .AddIdentityServer(options =>
                {
                    options.IssuerUri = "https://devkit.security/";
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddCustomStores()
                .AddCustomValidators()
                .AddDeveloperSigningCredential();

            services
                .AddIdentityMongoDbProvider<UserAccount, UserRole>(
                    identityOptions =>
                    {
                        identityOptions.Password.RequiredLength = 6;
                        identityOptions.Password.RequireLowercase = true;
                        identityOptions.Password.RequireUppercase = true;
                        identityOptions.Password.RequireNonAlphanumeric = true;
                        identityOptions.Password.RequireDigit = true;
                    },
                    mongoIdentityOptions =>
                    {
                        var connectionString = services.BuildServiceProvider().GetRequiredService<RepositoryOptions>().ConnectionString;
                        mongoIdentityOptions.ConnectionString = connectionString;
                    });

            services
                .AddAuthentication()
                .AddGoogle(options =>
                {
                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to http://localhost:5000/signin-google
                    options.ClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID");
                    options.ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_SECRET");
                })
                .AddFacebook(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    options.ClientId = Environment.GetEnvironmentVariable("FACEBOOK_CLIENT_ID");
                    options.ClientSecret = Environment.GetEnvironmentVariable("FACEBOOK_SECRET");
                });

            services.AddSeedData<SecuritySeeder>();
        }
    }
}