// -----------------------------------------------------------------------
// <copyright file="SecurityTestBase.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Test
{
    using System;
    using AspNetCore.Identity.Mongo;
    using AspNetCore.Identity.Mongo.Mongo;
    using AspNetCore.Identity.Mongo.Stores;
    using Devkit.Security;
    using Devkit.Security.Data.Models;
    using Devkit.Test;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Security API test base.
    /// </summary>
    /// <typeparam name="T">The type of test input.</typeparam>
    /// <seealso cref="IntegrationTestBase{T, Startup}" />
    public abstract class SecurityTestBase<T> : IntegrationTestBase<T, Startup>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityTestBase{T}"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        protected SecurityTestBase(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
            Environment.SetEnvironmentVariable("GOOGLE_CLIENT_ID", this.Faker.Random.AlphaNumeric(10));
            Environment.SetEnvironmentVariable("GOOGLE_SECRET", this.Faker.Random.AlphaNumeric(10));
            Environment.SetEnvironmentVariable("FACEBOOK_CLIENT_ID", this.Faker.Random.AlphaNumeric(10));
            Environment.SetEnvironmentVariable("FACEBOOK_SECRET", this.Faker.Random.AlphaNumeric(10));

            testFixture?.ConfigureTestServices(services =>
            {
                // Setup user manager and role manager for integration test overriding the default implementation.
                var databaseOptions = new MongoIdentityOptions
                {
                    ConnectionString = testFixture.RepositoryConfiguration.ConnectionString
                };

                var userCollection = MongoUtil.FromConnectionString<UserAccount>(databaseOptions.ConnectionString, databaseOptions.UsersCollection);
                var roleCollection = MongoUtil.FromConnectionString<UserRole>(databaseOptions.ConnectionString, databaseOptions.RolesCollection);

                services.AddSingleton(x => userCollection);
                services.AddSingleton(x => roleCollection);

                services.AddTransient<IUserStore<UserAccount>>(x =>
                    new UserStore<UserAccount, UserRole>(
                        userCollection,
                        new RoleStore<UserRole>(roleCollection),
                        x.GetService<ILookupNormalizer>()));

                services.AddTransient<IRoleStore<UserRole>>(x => new RoleStore<UserRole>(roleCollection));
            });
        }
    }
}