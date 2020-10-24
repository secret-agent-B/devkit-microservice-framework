// -----------------------------------------------------------------------
// <copyright file="CreateInvoiceValidator.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Payment.CoinsPH.Business.Invoice.Commands.CreateInvoice
{
    using FluentValidation;

    /// <summary>
    /// The CreateInvoiceValidator for creating an invoice for Coins.Ph.
    /// </summary>
    public class CreateInvoiceValidator : AbstractValidator<CreateInvoiceCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInvoiceValidator"/> class.
        /// </summary>
        public CreateInvoiceValidator()
        {
            this.RuleFor(x => x.Amount)
                .GreaterThan(0);

            this.RuleFor(x => x.Currency)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.TransactionId)
                .NotNull()
                .NotEmpty();
        }
    }
}