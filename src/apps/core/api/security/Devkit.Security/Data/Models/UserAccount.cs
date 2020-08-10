// -----------------------------------------------------------------------
// <copyright file="UserAccount.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Data.Models
{
    using System;
    using AspNetCore.Identity.Mongo.Model;

    /// <summary>
    /// A user's account information.
    /// </summary>
    public class UserAccount : MongoUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccount"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        public UserAccount(string userName)
            : base(userName)
        {
        }

        /// <summary>
        /// Gets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime CreatedOn { get; internal set; }

        /// <summary>
        /// Gets or sets the profile.
        /// </summary>
        /// <value>
        /// The profile.
        /// </value>
        public UserProfile Profile { get; set; }
    }
}