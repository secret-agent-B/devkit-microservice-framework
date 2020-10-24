// -----------------------------------------------------------------------
// <copyright file="UsersController.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Controllers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Devkit.Security.Business.Users.Commands.RegisterUser;
    using Devkit.Security.Business.ViewModels;
    using Devkit.WebAPI;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The users controller.
    /// </summary>
    /// <seealso cref="DevkitControllerBase" />
    [Route("[controller]")]
    public class UsersController : DevkitControllerBase
    {
        /// <summary>
        /// Registers the specified username.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="request">The request.</param>
        /// <returns>
        /// An register user response.
        /// </returns>
        [HttpPost("{role}/register/")]
        public async Task<UserVM> Register([FromRoute] string role, [FromBody][NotNull] RegisterUserCommand request)
        {
            request.Role = role;
            return await this.Mediator.Send(request);
        }
    }
}