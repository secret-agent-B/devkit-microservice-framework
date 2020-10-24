// -----------------------------------------------------------------------
// <copyright file="IPaymentVendorService.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Payment.Contracts
{
    using System.Threading.Tasks;
    using Devkit.Payment.ViewModels;
    using MediatR;

    /// <summary>
    /// The IPaymentVendorAPI.
    /// </summary>
    public interface IPaymentVendorService<TConfiguration>
        where TConfiguration : class
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        TConfiguration Configuration { get; }

        /// <summary>
        /// Gets the mediator.
        /// </summary>
        /// <value>
        /// The mediator.
        /// </value>
        IMediator Mediator { get; }

        /// <summary>
        /// Creates the invoice.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="currency">The currency.</param>
        /// <returns>A vendor transaction view model.</returns>
        Task<VendorTransactionVM> CreateInvoice(double amount, string transactionId, string currency);

        /// <summary>
        /// Gets the invoice status.
        /// </summary>
        /// <param name="invoiceId">The invoice identifier.</param>
        /// <returns>A vendor transaction view model.</returns>
        Task<VendorTransactionVM> GetInvoiceStatus(string invoiceId);
    }
}