// -----------------------------------------------------------------------
// <copyright file="CustomResourceStore.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using IdentityServer4.Models;
    using IdentityServer4.Stores;

    /// <summary>
    /// The custom resources store.
    /// </summary>
    /// <seealso cref="IResourceStore" />
    public class CustomResourceStore : IResourceStore
    {
        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomResourceStore" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public CustomResourceStore(IRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Gets API resources by API resource name.
        /// </summary>
        /// <param name="apiResourceNames">A collection of resource names.</param>
        /// <returns>A collection of ApiResources.</returns>
        public Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            var result = this._repository.GetMany<ApiResource>(a => apiResourceNames.Contains(a.Name));
            return Task.FromResult(result.AsEnumerable());
        }

        /// <summary>
        /// Gets API resources by scope name.
        /// </summary>
        /// <param name="scopeNames">A collection of scope names.</param>
        /// <returns>A collection of ApiResrouces.</returns>
        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var result = this._repository.GetMany<ApiResource>(a => a.Scopes.Any(s => scopeNames.Contains(s)));
            return Task.FromResult(result.AsEnumerable());
        }

        /// <summary>
        /// Gets API scopes by scope name.
        /// </summary>
        /// <param name="scopeNames">A collection of scope names.</param>
        /// <returns>A collection of ApiResrouces.</returns>
        public Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            var result = this._repository.GetMany<ApiScope>(a => scopeNames.Contains(a.Name));
            return Task.FromResult(result.AsEnumerable());
        }

        /// <summary>
        /// Gets identity resources by scope name.
        /// </summary>
        /// <param name="scopeNames">A collection of scope names.</param>
        /// <returns>A collection of IdentityResources.</returns>
        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var result = this._repository.GetMany<IdentityResource>(a => scopeNames.Contains(a.Name));
            return Task.FromResult(result.AsEnumerable());
        }

        /// <summary>
        /// Gets all resources.
        /// </summary>
        /// <returns>A Resource instance.</returns>
        public Task<Resources> GetAllResourcesAsync()
        {
            var identityResouces = this._repository.All<IdentityResource>();
            var apiResources = this._repository.All<ApiResource>();
            var apiScopes = this._repository.All<ApiScope>();

            return Task.FromResult(new Resources(identityResouces, apiResources, apiScopes));
        }
    }
}