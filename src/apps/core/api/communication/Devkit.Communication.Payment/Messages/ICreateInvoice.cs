// -----------------------------------------------------------------------
// <copyright file="ICreateInvoice.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.Payment.Messages
{
    using Devkit.Communication.Payment.DTOs;
    using Devkit.ServiceBus.Interfaces;

    /// <summary>
    /// The ICreateInvoice is the request that is sent to generate an invoice.
    /// </summary>
    public interface ICreateInvoice : IMessage<IInvoiceDTO>
    {
        /// <summary>
        /// Gets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        decimal Amount { get; }

        /// <summary>
        /// Gets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        string Currency { get; }

        /// <summary>
        /// Gets the payment vendor code.
        /// </summary>
        /// <value>
        /// The payment vendor code.
        /// </value>
        string PaymentVendorCode { get; }

        /// <summary>
        /// Gets the transaction identifier.
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        string TransactionId { get; }
    }
}