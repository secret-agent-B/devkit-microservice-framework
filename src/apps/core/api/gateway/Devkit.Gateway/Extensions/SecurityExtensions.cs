// -----------------------------------------------------------------------
// <copyright file="SecurityExtensions.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Gateway.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Devkit.Gateway.Configuration;
    using IdentityServer4.AccessTokenValidation;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Logging;
    using Serilog;

    /// <summary>
    /// The authorization extension.
    /// </summary>
    public static class SecurityExtensions
    {
        /// <summary>
        /// Adds the authorization to IdentityServer4 host.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>
        /// The service collection.
        /// </returns>
        public static IServiceCollection AddSecurity(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var securityOptions = configuration.GetSection(SecurityConfiguration._section).Get<SecurityConfiguration>();

            AddAuthentication(services, securityOptions.AuthenticationConfiguration);
            AddAuthorization(services, securityOptions.AuthorizationPolicies);

            return services;
        }

        /// <summary>
        /// Adds the authentication.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="authConfig">The authentication options.</param>
        private static void AddAuthentication(IServiceCollection services, AuthenticationConfiguration authConfig)
        {
            var logger = services.BuildServiceProvider().GetRequiredService<ILogger>();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(
                    authConfig.AuthenticationProviderKey,
                    options =>
                    {
                        // Introspection settings to validate token received from the client
                        // make sure that the JWT signature is still valid
                        options.ApiName = authConfig.APIResourceName;
                        options.ApiSecret = authConfig.APIResourceSecret;
                        options.Authority = authConfig.IdentityServerHost;
                        options.CacheDuration = TimeSpan.FromMinutes(authConfig.CacheDurationInMinutes);
                        options.EnableCaching = true;
                        options.RequireHttpsMetadata = authConfig.RequireHttpsMetadata;
                        options.RoleClaimType = authConfig.RoleClaimType;

                        if (Debugger.IsAttached)
                        {
                            IdentityModelEventSource.ShowPII = true;
                            options.JwtBearerEvents = new DebugJwtBearerEvents(logger);
                        }
                    })
                .AddJwtBearer(options =>
                {
                    options.Authority = authConfig.IdentityServerHost;
                    options.Audience = authConfig.APIResourceName;
                });
        }

        /// <summary>
        /// Adds the authorization.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="authorizationPolicies">The authorization policies.</param>
        private static void AddAuthorization(IServiceCollection services, ICollection<AuthorizationPolicy> authorizationPolicies)
        {
            services
                .AddMvcCore()
                .AddAuthorization(options =>
                {
                    foreach (var authorizationPolicy in authorizationPolicies)
                    {
                        options.AddPolicy(authorizationPolicy.Name, policy =>
                        {
                            foreach (var authorizationPolicyClaimRequirement in authorizationPolicy.ClaimRequirements)
                            {
                                policy.RequireClaim(
                                    authorizationPolicyClaimRequirement.Type,
                                    authorizationPolicyClaimRequirement.Requirements);
                            }
                        });
                    }
                });
        }

        /// <summary>
        /// The debug JWT bearer events.
        /// </summary>
        /// <seealso cref="JwtBearerEvents" />
        private class DebugJwtBearerEvents : JwtBearerEvents
        {
            /// <summary>
            /// The logger.
            /// </summary>
            private readonly ILogger _logger;

            /// <summary>
            /// Initializes a new instance of the <see cref="DebugJwtBearerEvents"/> class.
            /// </summary>
            /// <param name="logger">The logger.</param>
            public DebugJwtBearerEvents(ILogger logger)
            {
                this._logger = logger;
            }

            /// <summary>
            /// Authentications the failed.
            /// </summary>
            /// <param name="context">The context.</param>
            /// <returns>A task.</returns>
            public override Task AuthenticationFailed(AuthenticationFailedContext context)
            {
                this._logger
                    .ForContext("JWTDebugEvent", "AuthenticationFailed")
                    .ForContext("Context", context)
                    .Debug("JWT AuthenticationFailed");

                return base.AuthenticationFailed(context);
            }

            /// <summary>
            /// Challenges the specified context.
            /// </summary>
            /// <param name="context">The context.</param>
            /// <returns>A task.</returns>
            public override Task Challenge(JwtBearerChallengeContext context)
            {
                this._logger
                    .ForContext("JWTDebugEvent", "Challenge")
                    .ForContext("Context", context)
                    .Debug("JWT Challenge");

                return base.Challenge(context);
            }

            /// <summary>
            /// Messages the received.
            /// </summary>
            /// <param name="context">The context.</param>
            /// <returns>A task.</returns>
            public override Task MessageReceived(MessageReceivedContext context)
            {
                this._logger
                    .ForContext("JWTDebugEvent", "MessageReceived")
                    .ForContext("Context", context)
                    .Debug("JWT MessageReceived");

                return base.MessageReceived(context);
            }

            /// <summary>
            /// Tokens the validated.
            /// </summary>
            /// <param name="context">The context.</param>
            /// <returns>A task.</returns>
            public override Task TokenValidated(TokenValidatedContext context)
            {
                this._logger
                    .ForContext("JWTDebugEvent", "TokenValidated")
                    .ForContext("Context", context)
                    .Debug("JWT TokenValidated");

                return base.TokenValidated(context);
            }
        }
    }
}