// -----------------------------------------------------------------------
// <copyright file="FilesController.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.FileStore.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Devkit.FileStore.Business.Files.Commands.UploadFile;
    using Devkit.FileStore.Business.Files.Queries.DownloadFile;
    using Devkit.FileStore.Business.ViewModels;
    using Devkit.WebAPI;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

    /// <summary>
    /// The files controller.
    /// </summary>
    [Route("[controller]")]
    public class FilesController : DevkitControllerBase
    {
        /// <summary>
        /// Downloads the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// The file object.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Download([FromRoute] DownloadFileQuery request)
        {
            var fileVm = await this.Mediator.Send(request);
            return this.File(fileVm.Contents.ToArray(), "application/octet-stream", fileVm.FileName);
        }

        /// <summary>
        /// Uploads the specified files.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <returns>
        /// The ids of the uploaded files.
        /// </returns>
        [HttpPost("")]
        public async Task<IList<(string id, string fileName)>> Upload(IList<IFormFile> files)
        {
            var ids = new List<(string id, string fileName)>();

            foreach (var file in files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);

                    var response = await this.Mediator.Send(new UploadFileCommand
                    {
                        Contents = memoryStream.ToArray(),
                        FileName = file.FileName
                    });

                    ids.Add((response.Id, response.FileName));
                }
            }

            return ids;
        }
    }
}