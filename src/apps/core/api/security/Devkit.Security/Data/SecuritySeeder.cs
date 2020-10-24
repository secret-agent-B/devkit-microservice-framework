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
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.Data.Seeding;
    using Devkit.Security.Data.Models;
    using IdentityModel;
    using IdentityServer4.Models;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// The database seeder.
    /// </summary>
    /// <seealso cref="ExcelSeederBase" />
    public class SecuritySeeder : ExcelSeederBase
    {
        /// <summary>
        /// The days to expire.
        /// </summary>
        private const int daysToExpire = 2;

        /// <summary>
        /// The seconds within a day.
        /// </summary>
        private const int secondsWithinADay = 86400;

        /// <summary>
        /// The client permissions.
        /// </summary>
        private readonly List<string> _clientPermissions;

        /// <summary>
        /// The driver permissions.
        /// </summary>
        private readonly List<string> _driverPermissions;

        /// <summary>
        /// The role manager.
        /// </summary>
        private readonly RoleManager<UserRole> _roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecuritySeeder" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="seederConfig">The seeder configuration.</param>
        /// <param name="roleManager">The role manager.</param>
        public SecuritySeeder(IRepository repository, ISeederConfig seederConfig, RoleManager<UserRole> roleManager)
            : base(repository, seederConfig)
        {
            this._roleManager = roleManager;

            this._driverPermissions = new List<string>
            {
                "users.read",
                "deliveries.read",
                "deliveries.write",
                "orders.read",
                "orders.write",
                "vehicles.read",
                "vehicles.write",
                "files.read",
                "files.write"
            };

            this._clientPermissions = new List<string>
            {
                "users.read",
                "orders.read",
                "orders.write",
                "vehicles.read",
                "files.read",
                "files.write"
            };
        }

        /// <summary>
        /// Executes the seeding process.
        /// </summary>
        public async override Task Execute()
        {
            this.SeedClientConfiguration();

            await this.SeedRoles();
        }

        /// <summary>
        /// Seeds the client configuration.
        /// </summary>
        private void SeedClientConfiguration()
        {
            const string clientId = "mobile-app";
            const string clientSecret = "secret";
            const string apiGatewayName = "mobile-gateway";
            const string apiGatewayDisplayName = "Mobile Gateway";

            var apiGateway = new ApiResource(apiGatewayName, apiGatewayDisplayName);

            foreach (var scope in this._driverPermissions)
            {
                apiGateway.Scopes.Add(scope);
            }

            foreach (var scope in this._clientPermissions)
            {
                apiGateway.Scopes.Add(scope);
            }

            var client = new Client
            {
                ClientId = clientId,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                ClientSecrets =
                    {
                        new Secret(clientSecret.Sha256())
                    },
                AllowedScopes = apiGateway.Scopes,
                AccessTokenLifetime = secondsWithinADay * daysToExpire
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

        /// <summary>
        /// Seeds the roles.
        /// </summary>
        /// <returns>A task.</returns>
        private async Task SeedRoles()
        {
            const string driverRoleName = "Driver";
            const string clientRoleName = "Client";
            const string adminRoleName = "Administrator";
            const string permissionClaim = "permissions";

            // Driver role seed
            var driverRole = await this._roleManager.FindByNameAsync(driverRoleName);

            if (driverRole == null)
            {
                driverRole = new UserRole(driverRoleName);
                await this._roleManager.CreateAsync(driverRole);

                foreach (var permission in this._driverPermissions)
                {
                    await this._roleManager.AddClaimAsync(driverRole, new Claim(permissionClaim, permission));
                }
            }

            // Client role seed
            var clientRole = await this._roleManager.FindByNameAsync(clientRoleName);

            if (clientRole == null)
            {
                clientRole = new UserRole(clientRoleName);
                await this._roleManager.CreateAsync(clientRole);

                foreach (var permission in this._clientPermissions)
                {
                    await this._roleManager.AddClaimAsync(clientRole, new Claim(permissionClaim, permission));
                }
            }

            // Administrator role seed
            var administrator = await this._roleManager.FindByNameAsync(adminRoleName);

            if (administrator == null)
            {
                administrator = new UserRole(adminRoleName);
                await this._roleManager.CreateAsync(administrator);

                foreach (var permission in this._driverPermissions)
                {
                    await this._roleManager.AddClaimAsync(administrator, new Claim(permissionClaim, permission));
                }

                foreach (var permission in this._clientPermissions)
                {
                    await this._roleManager.AddClaimAsync(administrator, new Claim(permissionClaim, permission));
                }
            }
        }
    }
}