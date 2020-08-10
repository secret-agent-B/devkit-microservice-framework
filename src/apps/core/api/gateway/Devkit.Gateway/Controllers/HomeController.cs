// -----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Gateway.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// THe home controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>An action result.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return this.Json(new { message = "Hello world!" });
        }
    }
}