// -----------------------------------------------------------------------
// <copyright file="CustomClientStore.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Stores
{
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using IdentityServer4.Models;
    using IdentityServer4.Stores;

    /// <summary>
    /// The custom client store.
    /// </summary>
    /// <seealso cref="IClientStore" />
    public class CustomClientStore : IClientStore
    {
        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomClientStore"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public CustomClientStore(IRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Finds a client by id.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <returns>
        /// The client.
        /// </returns>
        public Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = this._repository.GetOneOrDefault<Client>(x => x.ClientId == clientId);

            return Task.FromResult(client);
        }
    }
}