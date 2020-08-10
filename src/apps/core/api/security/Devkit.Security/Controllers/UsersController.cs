// -----------------------------------------------------------------------
// <copyright file="UsersController.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Controllers
{
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
        /// <param name="request">The request.</param>
        /// <returns>
        /// An register user response.
        /// </returns>
        [HttpPost("register")]
        public async Task<UserVM> Register([FromBody] RegisterUserCommand request) => await this.Mediator.Send(request);
    }
}