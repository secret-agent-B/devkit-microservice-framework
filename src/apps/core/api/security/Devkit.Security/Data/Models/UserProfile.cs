// -----------------------------------------------------------------------
// <copyright file="UserProfile.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Data.Models
{
    /// <summary>
    /// The user profile.
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>
        /// The address1.
        /// </value>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address2.
        /// </summary>
        /// <value>
        /// The address2.
        /// </value>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>
        /// The name of the middle.
        /// </value>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the normalized first name.
        /// </summary>
        /// <value>
        /// The first name of the normalized.
        /// </value>
        public string NormalizedFirstName { get; set; }

        /// <summary>
        /// Gets or sets the full name of the normalized.
        /// </summary>
        /// <value>
        /// The full name of the normalized.
        /// </value>
        public string NormalizedFullName { get; set; }

        /// <summary>
        /// Gets or sets the normalized last name.
        /// </summary>
        /// <value>
        /// The last name of the normalized.
        /// </value>
        public string NormalizedLastName { get; set; }

        /// <summary>
        /// Gets or sets the name of the normalized middle.
        /// </summary>
        /// <value>
        /// The name of the normalized middle.
        /// </value>
        public string NormalizedMiddleName { get; set; }

        /// <summary>
        /// Gets or sets the province.
        /// </summary>
        /// <value>
        /// The province.
        /// </value>
        public string Province { get; set; }

        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>
        /// The zip.
        /// </value>
        public string Zip { get; set; }
    }
}