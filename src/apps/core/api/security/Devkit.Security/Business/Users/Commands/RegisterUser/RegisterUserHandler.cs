// -----------------------------------------------------------------------
// <copyright file="RegisterUserHandler.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Business.Users.Commands.RegisterUser
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS.Command;
    using Devkit.Security.Business.ViewModels;
    using Devkit.Security.Data.Models;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Handler for registering a new user.
    /// </summary>
    /// <seealso cref="CommandHandlerBase{RegisterUserCommand, RegisterUserResponse}" />
    public class RegisterUserHandler : CommandHandlerBase<RegisterUserCommand, UserVM>
    {
        /// <summary>
        /// The user role.
        /// </summary>
        private const string _userRole = "User";

        /// <summary>
        /// The role manager.
        /// </summary>
        private readonly RoleManager<UserRole> _roleManager;

        /// <summary>
        /// The user manager.
        /// </summary>
        private readonly UserManager<UserAccount> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterUserHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        public RegisterUserHandler(IRepository repository, UserManager<UserAccount> userManager, RoleManager<UserRole> roleManager)
            : base(repository)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var user = new UserAccount(this.Request.UserName)
            {
                CreatedOn = DateTime.Now,
                Email = this.Request.Email,
                PhoneNumber = this.Request.PhoneNumber,
                Profile = new UserProfile
                {
                    FirstName = this.Request.FirstName,
                    MiddleName = this.Request.MiddleName,
                    LastName = this.Request.LastName,
                    FullName = $"{this.Request.FirstName} {this.Request.MiddleName} {this.Request.LastName}",

                    NormalizedFirstName = this.Request.FirstName.ToUpperInvariant(),
                    NormalizedMiddleName = this.Request.MiddleName.ToUpperInvariant(),
                    NormalizedLastName = this.Request.LastName.ToUpperInvariant(),
                    NormalizedFullName
                        = $"{this.Request.FirstName} {this.Request.MiddleName} {this.Request.LastName}".ToUpperInvariant(),

                    Address1 = this.Request.Address1,
                    Address2 = this.Request.Address2,
                    City = this.Request.City,
                    Province = this.Request.Province,
                    Country = this.Request.Country,
                    Zip = this.Request.Zip
                }
            };

            var createResult = await this._userManager.CreateAsync(user, this.Request.Password);

            if (createResult.Succeeded)
            {
                if (await this._roleManager.FindByNameAsync(_userRole) == null)
                {
                    await this._roleManager.CreateAsync(new UserRole(_userRole));
                }

                await this._userManager.AddToRoleAsync(user, _userRole);

                this.Response.Id = user.Id.ToString();
                this.Response.UserName = user.UserName;
                this.Response.FirstName = user.Profile.FirstName;
                this.Response.LastName = user.Profile.LastName;
                this.Response.CreatedOn = user.CreatedOn;
                this.Response.Address1 = user.Profile.Address1;
                this.Response.Address2 = user.Profile.Address2;
                this.Response.City = user.Profile.City;
                this.Response.Country = user.Profile.Country;
                this.Response.Province = user.Profile.Province;
                this.Response.Zip = user.Profile.Zip;
                this.Response.PhoneNumber = user.PhoneNumber;
            }
            else
            {
                foreach (var error in createResult.Errors)
                {
                    this.Response.Exceptions.Add(error.Code, new[] { error.Description });
                }
            }
        }
    }
}