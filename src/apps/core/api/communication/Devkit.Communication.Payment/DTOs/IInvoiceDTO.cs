// -----------------------------------------------------------------------
// <copyright file="IInvoiceDTO.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.Payment.DTOs
{
    /// <summary>
    /// The IInvoiceDTO is the data transfer object for invoices.
    /// </summary>
    public interface IInvoiceDTO
    {
        /// <summary>
        /// Gets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        decimal Amount { get; }

        /// <summary>
        /// Gets the invoice identifier.
        /// </summary>
        /// <value>
        /// The invoice identifier.
        /// </value>
        string InvoiceId { get; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        string Status { get; }

        /// <summary>
        /// Gets the transaction identifier.
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        string TransactionId { get; }
    }
}