// -----------------------------------------------------------------------
// <copyright file="UploadFileValidator.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.FileStore.Business.Files.Commands.UploadFile
{
    using FluentValidation;

    /// <summary>
    /// The validator for UploadFileCommand.
    /// </summary>
    public class UploadFileValidator : AbstractValidator<UploadFileCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadFileValidator"/> class.
        /// </summary>
        public UploadFileValidator()
        {
            this.RuleFor(x => x.Contents)
                .NotEmpty()
                .NotNull();

            this.RuleFor(x => x.FileName)
                .NotEmpty()
                .NotNull();
        }
    }
}