// -----------------------------------------------------------------------
// <copyright file="Intg_RegisterUser.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Test.CQRS.Users.Commands.RegisterUser
{
    using System.Net;
    using System.Threading.Tasks;
    using Bogus;
    using Devkit.Security;
    using Devkit.Security.Business.Users.Commands.RegisterUser;
    using Devkit.Security.Business.ViewModels;
    using Devkit.Test;
    using Xunit;

    /// <summary>
    /// The register user integration test.
    /// </summary>
    public class Intg_RegisterUser : SecurityTestBase<RegisterUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_RegisterUser"/> class.
        /// </summary>
        /// <param name="appTestFixture">The application test fixture.</param>
        public Intg_RegisterUser(AppTestFixture<Startup> appTestFixture)
            : base(appTestFixture)
        {
        }

        /// <summary>
        /// Fails if client confirm password doesn't match password.
        /// </summary>
        /// <returns>A task.</returns>
        [Fact(DisplayName = "Fails if client confirm password doesn't match password")]
        public async Task Fail_if_client_confirm_password_doesnt_match_password()
        {
            var command = this.Build();
            command.ConfirmPassword = "SomeTestPassword123";

            var response = await this.PostAsync<UserVM>("/users/client/register", command);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// Fails if client password is empty.
        /// </summary>
        /// <returns>A task.</returns>
        [Fact(DisplayName = "Fails if client password is empty")]
        public async Task Fail_if_client_password_is_empty()
        {
            var command = this.Build();
            command.Password = string.Empty;

            var response = await this.PostAsync<UserVM>("/users/client/register", command);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// Fails if client username is empty.
        /// </summary>
        /// <returns>A task.</returns>
        [Fact(DisplayName = "Fails if username is empty")]
        public async Task Fail_if_client_username_is_empty()
        {
            var command = this.Build();
            command.UserName = string.Empty;

            var response = await this.PostAsync<UserVM>("/users/client/register", command);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// Fails if driver confirm password doesn't match password.
        /// </summary>
        /// <returns>A task.</returns>
        [Fact(DisplayName = "Fails if driver confirm password doesn't match password")]
        public async Task Fail_if_driver_confirm_password_doesnt_match_password()
        {
            var command = this.Build();
            command.ConfirmPassword = "SomeTestPassword123";

            var response = await this.PostAsync<UserVM>("/users/driver/register", command);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// Fails if driver password is empty.
        /// </summary>
        /// <returns>A task.</returns>
        [Fact(DisplayName = "Fails if driver password is empty")]
        public async Task Fail_if_driver_password_is_empty()
        {
            var command = this.Build();
            command.Password = string.Empty;

            var response = await this.PostAsync<UserVM>("/users/driver/register", command);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// Fails if driver username is empty.
        /// </summary>
        /// <returns>A task.</returns>
        [Fact(DisplayName = "Fails if username is empty")]
        public async Task Fail_if_driver_username_is_empty()
        {
            var command = this.Build();
            command.UserName = string.Empty;

            var response = await this.PostAsync<UserVM>("/users/driver/register", command);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// Passes if client registration was successful.
        /// </summary>
        /// <returns>A task.</returns>
        [Fact(DisplayName = "Passes if client registration was successful")]
        public async Task Pass_if_client_registration_was_successful()
        {
            var response = await this.PostAsync<UserVM>("/users/client/register", this.Build(), true);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Passes if driver registration was successful.
        /// </summary>
        /// <returns>A task.</returns>
        [Fact(DisplayName = "Passes if driver registration was successful")]
        public async Task Pass_if_driver_registration_was_successful()
        {
            var response = await this.PostAsync<UserVM>("/users/driver/register", this.Build(), true);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Builds a request.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override RegisterUserCommand Build()
        {
            var password = "Passw0rd123$";
            var commandFaker = new Faker<RegisterUserCommand>();

            commandFaker
                .RuleFor(x => x.UserName, f => f.Person.UserName)
                .RuleFor(x => x.FirstName, f => f.Person.FirstName)
                .RuleFor(x => x.MiddleName, f => f.Person.FirstName)
                .RuleFor(x => x.LastName, f => f.Person.LastName)
                .RuleFor(x => x.Password, password)
                .RuleFor(x => x.ConfirmPassword, password)
                .RuleFor(x => x.Address1, f => f.Address.StreetAddress())
                .RuleFor(x => x.Address2, f => f.Address.SecondaryAddress())
                .RuleFor(x => x.City, f => f.Address.City())
                .RuleFor(x => x.Province, f => f.Address.County())
                .RuleFor(x => x.Country, f => f.Address.Country())
                .RuleFor(x => x.ZipCode, f => f.Address.ZipCode())
                .RuleFor(x => x.PhoneNumber, f => f.Person.Phone);

            return commandFaker.Generate();
        }

        /// <summary>
        /// Seeds the database.
        /// </summary>
        protected override void SeedDatabase()
        {
        }
    }
}