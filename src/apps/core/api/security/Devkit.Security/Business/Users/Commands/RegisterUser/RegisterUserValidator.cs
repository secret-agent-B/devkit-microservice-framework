// -----------------------------------------------------------------------
// <copyright file="RegisterUserValidator.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Business.Users.Commands.RegisterUser
{
    using FluentValidation;

    /// <summary>
    /// Validator for RegisterUserCommand.
    /// </summary>
    /// <seealso cref="AbstractValidator{RegisterUserCommand}" />
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterUserValidator"/> class.
        /// </summary>
        public RegisterUserValidator()
        {
            this.RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100);

            this.RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(100);

            this.RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage("Confirm password must be equal to the given password.");

            this.RuleFor(x => x.Address1)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5);

            this.RuleFor(x => x.City)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);

            this.RuleFor(x => x.Province)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);

            this.RuleFor(x => x.Country)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);

            this.RuleFor(x => x.ZipCode)
                .NotNull()
                .NotEmpty()
                .MinimumLength(4);
        }
    }
}