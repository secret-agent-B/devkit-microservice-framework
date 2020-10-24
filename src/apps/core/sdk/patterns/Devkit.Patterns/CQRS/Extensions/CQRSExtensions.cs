// -----------------------------------------------------------------------
// <copyright file="CQRSExtensions.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.CQRS.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Devkit.Patterns.CQRS.Behaviors;
    using FluentValidation.AspNetCore;
    using MediatR;
    using MediatR.Pipeline;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// CQRS pattern extensions.
    /// </summary>
    public static class CQRSExtensions
    {
        /// <summary>
        /// Adds the MediatR assemblies that contains the CQRS classes. To override a behavior of a command
        /// by introducing a new library, make sure to place the overriding assembly before the assembly that is being
        /// overriden.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="assemblies">The assemblies.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddMediatRAssemblies(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            if (assemblies.Any())
            {
                services.AddMediatR(assemblies.ToArray());

                services.AddTransient(typeof(IRequestPreProcessor<>), typeof(RequestLoggerBehavior<>));
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(HandlerPerformanceBehavior<,>));
                services.AddTransient(typeof(IRequestPostProcessor<,>), typeof(ResponseBehavior<,>));
            }

            return services;
        }

        /// <summary>
        /// Adds the MediatR handler that contains the CQRS classes. To override a behavior of a command
        /// by introducing a new library, make sure to place the overriding assembly before the assembly that is being
        /// overriden.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="handlers">The handlers.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddMediatRHandlers(this IServiceCollection services, IEnumerable<Type> handlers, Action<MediatRServiceConfiguration> configuration)
        {
            if (handlers.Any())
            {
                services.AddMediatR(handlers, configuration);

                services.AddTransient(typeof(IRequestPreProcessor<>), typeof(RequestLoggerBehavior<>));
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(HandlerPerformanceBehavior<,>));
                services.AddTransient(typeof(IRequestPostProcessor<,>), typeof(ResponseBehavior<,>));
            }

            return services;
        }

        /// <summary>
        /// Adds the fluent validation.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="assemblies">The assemblies.</param>
        /// <returns>An MVC builder instance.</returns>
        public static IMvcBuilder AddValidationAssemblies(this IMvcBuilder builder, IEnumerable<Assembly> assemblies)
        {
            if (assemblies.Any())
            {
                builder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblies(assemblies));
            }

            return builder;
        }
    }
}