// -----------------------------------------------------------------------
// <copyright file="DownloadFileValidator.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.FileStore.Business.Files.Queries.DownloadFile
{
    using FluentValidation;

    /// <summary>
    /// Validator for DownloadFileQuery.
    /// </summary>
    /// <seealso cref="AbstractValidator{DownloadFileQuery}" />
    public class DownloadFileValidator : AbstractValidator<DownloadFileQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadFileValidator"/> class.
        /// </summary>
        public DownloadFileValidator()
        {
            this.RuleFor(x => x.Id)
                .Length(24);
        }
    }
}