// -----------------------------------------------------------------------
// <copyright file="SecuritySeeder.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Devkit.Data.Interfaces;
    using Devkit.Data.Seeding;
    using IdentityModel;
    using IdentityServer4.Models;

    /// <summary>
    /// The database seeder.
    /// </summary>
    /// <seealso cref="ExcelSeederBase" />
    public class SecuritySeeder : ExcelSeederBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecuritySeeder" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="seederConfig">The seeder configuration.</param>
        public SecuritySeeder(IRepository repository, ISeederConfig seederConfig)
            : base(repository, seederConfig)
        {
        }

        /// <summary>
        /// Executes the seeding process.
        /// </summary>
        public override void Execute()
        {
            const string clientId = "mobile-app";
            const string clientSecret = "secret";
            const string apiGatewayName = "mobile-gateway";
            const string apiGatewayDisplayName = "Mobile Gateway";

            var apiGateway = new ApiResource(apiGatewayName, apiGatewayDisplayName)
            {
                Scopes = { "client.read", "client.write", "client.delete" }
            };

            var client = new Client
            {
                ClientId = clientId,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                ClientSecrets =
                    {
                        new Secret(clientSecret.Sha256())
                    },
                AllowedScopes = apiGateway.Scopes
            };

            if (this.Repository.GetOneOrDefault<Client>(x => x.ClientId == client.ClientId) == null)
            {
                // step 1 - add clients
                this.Repository.Add(client);
            }

            // step 2 - add identity resources
            var identityResources = new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "roles",
                    DisplayName = "Roles",
                    Description = "Allow the service access to your user roles.",
                    UserClaims = new[] { JwtClaimTypes.Role, ClaimTypes.Role },
                    ShowInDiscoveryDocument = true,
                    Required = true,
                    Emphasize = true
                }
            };

            var existingResources = this.Repository.All<IdentityResource>().ToList();

            foreach (var identityResource in identityResources)
            {
                if (!existingResources.Any(x => x.Name == identityResource.Name))
                {
                    this.Repository.Add(identityResource);
                }
            }

            // step 3 - add api resources
            if (this.Repository.GetOneOrDefault<ApiResource>(x => x.Name == apiGateway.Name) == null)
            {
                this.Repository.Add(apiGateway);
            }

            // step 4 - add scopes
            foreach (var scope in apiGateway.Scopes)
            {
                var apiScope = new ApiScope(scope, scope + " scope");

                if (this.Repository.GetOneOrDefault<ApiScope>(x => x.Name == apiScope.Name) == null)
                {
                    this.Repository.Add(apiScope);
                }
            }
        }
    }
}