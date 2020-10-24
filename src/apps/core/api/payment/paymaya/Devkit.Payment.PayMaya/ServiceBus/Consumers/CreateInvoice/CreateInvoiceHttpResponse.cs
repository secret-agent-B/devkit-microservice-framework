// -----------------------------------------------------------------------
// <copyright file="CreateInvoiceResponse.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Payment.PayMaya.ServiceBus.Consumers.CreateInvoice
{
    /// <summary>
    /// The InvoiceResponse is the response from PayMaya.
    /// </summary>
    public class CreateInvoiceHttpResponse
    {
        /// <summary>
        /// Gets or sets the invoice identifier.
        /// </summary>
        /// <value>
        /// The invoice identifier.
        /// </value>
        public string InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the invoice URL.
        /// </summary>
        /// <value>
        /// The invoice URL.
        /// </value>
        public string InvoiceUrl { get; set; }
    }
}