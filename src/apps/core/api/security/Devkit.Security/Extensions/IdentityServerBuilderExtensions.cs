// -----------------------------------------------------------------------
// <copyright file="IdentityServerBuilderExtensions.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Extensions
{
    using Devkit.Security.Data.Models;
    using Devkit.Security.Stores;
    using IdentityServer4.AspNetIdentity;
    using IdentityServer4.Services;
    using IdentityServer4.Stores;
    using IdentityServer4.Validation;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Identity server builder extensions methods.
    /// </summary>
    internal static class IdentityServerBuilderExtensions
    {
        /// <summary>
        /// Adds the custom stores.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// An identity server builder.
        /// </returns>
        internal static IIdentityServerBuilder AddCustomStores(this IIdentityServerBuilder builder)
        {
            builder.Services.AddTransient<IClientStore, CustomClientStore>();
            builder.Services.AddTransient<ICorsPolicyService, InMemoryCorsPolicyService>();
            builder.Services.AddTransient<IResourceStore, CustomResourceStore>();
            builder.Services.AddSingleton<IPersistedGrantStore, CustomPersistedGrantStore>();
            builder.Services.AddSingleton<CustomUserStore>();

            builder.AddProfileService<CustomProfileService>();

            return builder;
        }

        /// <summary>
        /// Adds the validators.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// An identity server builder.
        /// </returns>
        internal static IIdentityServerBuilder AddCustomValidators(this IIdentityServerBuilder builder)
        {
            builder.Services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator<UserAccount>>();

            return builder;
        }
    }
}