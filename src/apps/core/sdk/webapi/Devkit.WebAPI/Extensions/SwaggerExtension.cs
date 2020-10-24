// -----------------------------------------------------------------------
// <copyright file="SwaggerExtension.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.WebAPI.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Reflection;
    using Devkit.WebAPI;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    /// <summary>
    /// The Open API 3 extension methods class.
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// Adds the swagger.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="apiDefinition">The API definition.</param>
        /// <returns>
        /// The service collection.
        /// </returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services, [NotNull] APIDefinition apiDefinition)
        {
            services.AddSwagger(apiDefinition.Name, apiDefinition.Description, apiDefinition.Version);

            return services;
        }

        /// <summary>
        /// Adds the Swagger service.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="version">The version.</param>
        /// <returns>
        /// The service collection.
        /// </returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services, string title, string description, string version)
        {
            _ = bool.TryParse(Environment.GetEnvironmentVariable("ENABLE_SWAGGER"), out var enableMiddleware);

            if (!enableMiddleware)
            {
                return services;
            }

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    version,
                    new OpenApiInfo
                    {
                        Title = title,
                        Description = description
                    });

                // Add documentation into Swagger.
                var xmlFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });

            return services;
        }

        /// <summary>
        /// Uses the swagger.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="apiDefinition">The API definition.</param>
        /// <returns>
        /// The application builder.
        /// </returns>
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, APIDefinition apiDefinition)
        {
            _ = bool.TryParse(Environment.GetEnvironmentVariable("ENABLE_SWAGGER"), out var enableMiddleware);

            if (!enableMiddleware)
            {
                return app;
            }

            if (apiDefinition == null)
            {
                throw new ArgumentNullException(nameof(apiDefinition));
            }

            return app.UseSwagger(apiDefinition.Name, apiDefinition.Version);
        }

        /// <summary>
        /// Uses the swagger.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="title">The title.</param>
        /// <param name="version">The version.</param>
        /// <returns>
        /// The application builder.
        /// </returns>
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, string title, string version)
        {
            _ = bool.TryParse(Environment.GetEnvironmentVariable("ENABLE_SWAGGER"), out var enableMiddleware);

            if (!enableMiddleware)
            {
                return app;
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint($"/swagger/{version}/swagger.json", title);
            });

            return app;
        }
    }
}