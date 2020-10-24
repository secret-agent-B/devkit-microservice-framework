// -----------------------------------------------------------------------
// <copyright file="CreateInvoiceCommand.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Payment.CoinsPH.Business.Invoice.Commands.CreateInvoice
{
    using Devkit.Patterns.CQRS.Command;
    using Devkit.Payment.ViewModels;

    /// <summary>
    /// The CreateInvoiceCommand is the command that is issued when the user initiates a transaction to buy more credits.
    /// </summary>
    public class CreateInvoiceCommand : CommandRequestBase<VendorTransactionVM>
    {
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public double Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        public string TransactionId { get; set; }
    }
}