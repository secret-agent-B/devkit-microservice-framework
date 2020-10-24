// -----------------------------------------------------------------------
// <copyright file="CustomUserStore.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Stores
{
    using System.Collections.Generic;
    using System.Linq;
    using AspNetCore.Identity.Mongo.Stores;
    using Devkit.Security.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using MongoDB.Driver;

    /// <summary>
    /// The CustomUserStore provides additional functionality for pulling users form the database.
    /// </summary>
    /// <seealso cref="UserStore{UserAccount, UserRole}" />
    public class CustomUserStore : UserStore<UserAccount, UserRole>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomUserStore"/> class.
        /// </summary>
        /// <param name="userCollection">The user collection.</param>
        /// <param name="roleStore">The role store.</param>
        /// <param name="normalizer">The normalizer.</param>
        public CustomUserStore(
            IMongoCollection<UserAccount> userCollection,
            IRoleStore<UserRole> roleStore,
            ILookupNormalizer normalizer)
            : base(userCollection, roleStore, normalizer)
        {
        }

        /// <summary>
        /// Gets the name of the users by.
        /// </summary>
        /// <param name="userNames">The user names.</param>
        /// <returns>
        /// A list of user accounts.
        /// </returns>
        public IList<UserAccount> FindByNames(IList<string> userNames)
        {
            var userAccounts = this.Users.Where(x => userNames.Contains(x.UserName));
            return userAccounts.ToList();
        }
    }
}