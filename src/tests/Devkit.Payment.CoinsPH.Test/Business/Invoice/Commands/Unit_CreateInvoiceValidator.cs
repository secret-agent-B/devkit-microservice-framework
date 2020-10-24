// -----------------------------------------------------------------------
// <copyright file="Unit_CreateInvoiceValidator.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Payment.CoinsPH.Test.Business.Invoice.Commands
{
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Devkit.Payment.CoinsPH.Business.Invoice.Commands.CreateInvoice;
    using Xunit;

    /// <summary>
    /// The Unit_CreateInvoiceValidator is CreateInvoiceValidator's unit test class.
    /// </summary>
    public class Unit_CreateInvoiceValidator : UnitTestBase<CreateInvoiceValidator>
    {
        /// <summary>
        /// Should fail if amount is invalid.
        /// </summary>
        /// <param name="amount">The amount.</param>
        [Theory(DisplayName = "Should fail if amount is invalid")]
        [InlineData(0.00)]
        [InlineData(-1.00)]
        public void Should_fail_if_amount_is_invalid(double amount)
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.Amount, amount);
        }

        /// <summary>
        /// Should fail if amount is invalid.
        /// </summary>
        /// <param name="currency">The currency.</param>
        [Theory(DisplayName = "Should fail if currency is invalid")]
        [InlineData("")]
        [InlineData(null)]
        public void Should_fail_if_currency_is_invalid(string currency)
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.Currency, currency);
        }

        /// <summary>
        /// Should fail if transaction identifier is missing.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        [Theory(DisplayName = "Should fail if transaction identifier is missing")]
        [InlineData("")]
        [InlineData(null)]
        public void Should_fail_if_transaction_id_is_missing(string transactionId)
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.TransactionId, transactionId);
        }

        /// <summary>
        /// Should pass if command is valid.
        /// </summary>
        [Fact(DisplayName = "Should pass if command is valid")]
        public void Should_pass_if_command_is_valid()
        {
            var command = new CreateInvoiceCommandFaker().Generate();
            var validator = this.Build();

            validator.ShouldNotHaveValidationErrorFor(x => x.Amount, command);
            validator.ShouldNotHaveValidationErrorFor(x => x.Currency, command);
            validator.ShouldNotHaveValidationErrorFor(x => x.TransactionId, command);
        }
    }
}