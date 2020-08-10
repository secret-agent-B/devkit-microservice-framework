// -----------------------------------------------------------------------
// <copyright file="CustomPersistedGrantStore.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Stores
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using IdentityServer4.Models;
    using IdentityServer4.Stores;

    /// <summary>
    /// The custom persisted grant store.
    /// </summary>
    /// <seealso cref="IPersistedGrantStore" />
    public class CustomPersistedGrantStore : IPersistedGrantStore
    {
        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPersistedGrantStore" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public CustomPersistedGrantStore(IRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Gets all grants based on the filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>A collection of persisted grants.</returns>
        public Task<IEnumerable<PersistedGrant>> GetAllAsync(PersistedGrantFilter filter)
        {
            var result = this._repository.GetMany<PersistedGrant>(x
                => filter.ClientId == x.ClientId
                && filter.SessionId == x.SessionId
                && filter.SubjectId == x.SubjectId
                && filter.Type == x.Type);

            return Task.FromResult(result.AsEnumerable());
        }

        /// <summary>
        /// Gets the grant.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A task.</returns>
        public Task<PersistedGrant> GetAsync(string key)
        {
            var result = this._repository.GetOneOrDefault<PersistedGrant>(i => i.Key == key);
            return Task.FromResult(result);
        }

        /// <summary>
        /// Removes all grants based on the filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>A task.</returns>
        public Task RemoveAllAsync(PersistedGrantFilter filter)
        {
            this._repository.Delete<PersistedGrantFilter>(x => true);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Removes the grant by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A task.</returns>
        public Task RemoveAsync(string key)
        {
            this._repository.Delete<PersistedGrant>(i => i.Key == key);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Stores the grant.
        /// </summary>
        /// <param name="grant">The grant.</param>
        /// <returns>A task.</returns>
        public Task StoreAsync(PersistedGrant grant)
        {
            this._repository.Add(grant);
            return Task.FromResult(0);
        }
    }
}