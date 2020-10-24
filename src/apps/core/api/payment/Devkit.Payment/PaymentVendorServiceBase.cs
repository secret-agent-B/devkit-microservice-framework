// -----------------------------------------------------------------------
// <copyright file="PaymentVendorServiceBase.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Payment
{
    using System.Threading.Tasks;
    using Devkit.Payment.Contracts;
    using Devkit.Payment.ViewModels;
    using MediatR;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// The VendorAPIBase is the base class for all payment vendors.
    /// </summary>
    public abstract class PaymentVendorServiceBase<TConfiguration> : IPaymentVendorService<TConfiguration>
        where TConfiguration : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentVendorServiceBase{TConfiguration}" /> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="options">The options.</param>
        protected PaymentVendorServiceBase(IMediator mediator, IOptions<TConfiguration> options)
        {
            this.Mediator = mediator;
            this.Configuration = options?.Value;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public TConfiguration Configuration { get; }

        /// <summary>
        /// Gets the mediator.
        /// </summary>
        /// <value>
        /// The mediator.
        /// </value>
        public IMediator Mediator { get; }

        /// <summary>
        /// Creates the invoice.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="currency">The currency.</param>
        /// <returns>A vendor transaction view model.</returns>
        public abstract Task<VendorTransactionVM> CreateInvoice(double amount, string transactionId, string currency);

        /// <summary>
        /// Gets the invoice status.
        /// </summary>
        /// <param name="invoiceId">The invoice identifier.</param>
        /// <returns>A vendor transaction view model.</returns>
        public abstract Task<VendorTransactionVM> GetInvoiceStatus(string invoiceId);
    }
}