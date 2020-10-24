// -----------------------------------------------------------------------
// <copyright file="CoinsPHService.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Payment.CoinsPH
{
    using System;
    using System.Threading.Tasks;
    using Devkit.Payment.CoinsPH.Business.Invoice.Commands.CreateInvoice;
    using Devkit.Payment.ViewModels;
    using MediatR;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// The CoinsPhAPI.
    /// </summary>
    public class CoinsPHService : PaymentVendorServiceBase<CoinsPHConfiguration>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoinsPHService"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="options">The options.</param>
        public CoinsPHService(IMediator mediator, IOptions<CoinsPHConfiguration> options)
            : base(mediator, options)
        {
        }

        /// <summary>
        /// Creates the invoice.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="currency">The currency.</param>
        /// <returns>
        /// A vendor transaction view model.
        /// </returns>
        public override async Task<VendorTransactionVM> CreateInvoice(double amount, string transactionId, string currency)
        {
            var response = await this.Mediator.Send(new CreateInvoiceCommand
            {
                Amount = amount,
                TransactionId = transactionId,
                Currency = currency
            });

            return response;
        }

        /// <summary>
        /// Gets the invoice status.
        /// </summary>
        /// <param name="invoiceId">The invoice identifier.</param>
        /// <returns>
        /// A vendor transaction view model.
        /// </returns>
        public override Task<VendorTransactionVM> GetInvoiceStatus(string invoiceId)
        {
            throw new NotImplementedException();
        }
    }
}