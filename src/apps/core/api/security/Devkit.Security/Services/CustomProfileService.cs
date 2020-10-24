// -----------------------------------------------------------------------
// <copyright file="CustomProfileService.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Devkit.Security.Data.Models;
    using IdentityModel;
    using IdentityServer4.Extensions;
    using IdentityServer4.Models;
    using IdentityServer4.Services;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// The custom profile service.
    /// </summary>
    /// <seealso cref="IProfileService" />
    public class CustomProfileService : IProfileService
    {
        /// <summary>
        /// The claims principal factory.
        /// </summary>
        private readonly IUserClaimsPrincipalFactory<UserAccount> _claimsPrincipalFactory;

        /// <summary>
        /// The role manager.
        /// </summary>
        private readonly RoleManager<UserRole> _roleManager;

        /// <summary>
        /// The user manager.
        /// </summary>
        private readonly UserManager<UserAccount> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomProfileService" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="claimsPrincipalFactory">The claims principal factory.</param>
        public CustomProfileService(UserManager<UserAccount> userManager, RoleManager<UserRole> roleManager, IUserClaimsPrincipalFactory<UserAccount> claimsPrincipalFactory)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._claimsPrincipalFactory = claimsPrincipalFactory;
        }

        /// <summary>
        /// This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A task.</returns>
        public async Task GetProfileDataAsync([NotNull] ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await this._userManager.FindByIdAsync(sub);
            var principal = await this._claimsPrincipalFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            // Add custom claims in token here based on user properties or any other source
            claims.Add(new Claim("firstName", user.Profile.FirstName ?? string.Empty));
            claims.Add(new Claim("lastName", user.Profile.LastName ?? string.Empty));
            claims.Add(new Claim("middleName", user.Profile.MiddleName ?? string.Empty));
            claims.Add(new Claim("fullName", user.Profile.FullName ?? string.Empty));
            claims.Add(new Claim("email", user.Email ?? string.Empty));
            claims.Add(new Claim("phone", user.PhoneNumber ?? string.Empty));
            claims.Add(new Claim("address1", user.Profile.Address1 ?? string.Empty));
            claims.Add(new Claim("address2", user.Profile.Address2 ?? string.Empty));
            claims.Add(new Claim("city", user.Profile.City ?? string.Empty));
            claims.Add(new Claim("province", user.Profile.Province ?? string.Empty));
            claims.Add(new Claim("country", user.Profile.Country ?? string.Empty));
            claims.Add(new Claim("zipcode", user.Profile.ZipCode ?? string.Empty));

            var roleNames = await this._userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (string roleName in roleNames)
            {
                roleClaims.Add(new Claim(JwtClaimTypes.Role, roleName));

                var role = await this._roleManager.FindByNameAsync(roleName);
                var permissions = await this._roleManager.GetClaimsAsync(role);

                claims.AddRange(permissions);
            }

            claims.AddRange(roleClaims);

            context.IssuedClaims = claims;
        }

        /// <summary>
        /// This method gets called whenever identity server needs to determine if the user is valid or active (e.g. if the user's account has been deactivated since they logged in).
        /// (e.g. during token issuance or validation).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A task.</returns>
        public async Task IsActiveAsync([NotNull] IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await this._userManager.FindByIdAsync(sub);

            context.IsActive = user != null;
        }
    }
}