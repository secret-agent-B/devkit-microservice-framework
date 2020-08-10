// -----------------------------------------------------------------------
// <copyright file="UserRole.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Data.Models
{
    using AspNetCore.Identity.Mongo.Model;

    /// <summary>
    /// A user's role within the system.
    /// </summary>
    public class UserRole : MongoRole
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRole"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public UserRole(string name)
            : base(name)
        {
        }
    }
}