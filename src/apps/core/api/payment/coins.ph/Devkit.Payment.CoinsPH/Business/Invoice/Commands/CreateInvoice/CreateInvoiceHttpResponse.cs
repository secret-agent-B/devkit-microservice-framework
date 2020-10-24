// -----------------------------------------------------------------------
// <copyright file="CreateInvoiceHttpResponse.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Payment.CoinsPH.Business.Invoice.Commands.CreateInvoice
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The CreateInvoiceResponse represents a response from CoinsPH after creating an invoice.
    /// </summary>
    public class CreateInvoiceHttpResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInvoiceHttpResponse"/> class.
        /// </summary>
        public CreateInvoiceHttpResponse()
        {
            this.SupportedPaymentCollectors = new List<string>();
        }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public double Amount { get; set; }

        /// <summary>
        /// Gets or sets the amount due.
        /// </summary>
        /// <value>
        /// The amount due.
        /// </value>
        [JsonProperty("amount_due")]
        public double AmountDue { get; set; }

        /// <summary>
        /// Gets or sets the BTC amount due.
        /// </summary>
        /// <value>
        /// The BTC amount due.
        /// </value>
        [JsonProperty("btc_amount_due")]
        public double BTCAmountDue { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the current payment reference number.
        /// </summary>
        /// <value>
        /// The current payment reference number.
        /// </value>
        [JsonProperty("current_payment_reference_number")]
        public string CurrentPaymentReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the expires at.
        /// </summary>
        /// <value>
        /// The expires at.
        /// </value>
        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Gets or sets the expires in seconds.
        /// </summary>
        /// <value>
        /// The expires in seconds.
        /// </value>
        [JsonProperty("expires_in_seconds")]
        public int ExpiresInSeconds { get; set; }

        /// <summary>
        /// Gets or sets the external transaction.
        /// </summary>
        /// <value>
        /// The external transaction.
        /// </value>
        [JsonProperty("external_transaction")]
        public string ExternalTransaction { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the incoming address.
        /// </summary>
        /// <value>
        /// The incoming address.
        /// </value>
        [JsonProperty("incoming_address")]
        public string IncomingAddress { get; set; }

        /// <summary>
        /// Gets or sets the initial rate.
        /// </summary>
        /// <value>
        /// The initial rate.
        /// </value>
        [JsonProperty("initial_rate")]
        public double InitialRate { get; set; }

        /// <summary>
        /// Gets or sets the locked rate.
        /// </summary>
        /// <value>
        /// The locked rate.
        /// </value>
        [JsonProperty("locked_rate")]
        public double LockedRate { get; set; }

        /// <summary>
        /// Gets or sets the meta data.
        /// </summary>
        /// <value>
        /// The meta data.
        /// </value>
        [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Used for deserializing vendor data.")]
        public JObject Metadata { get; set; }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        /// <value>
        /// The note.
        /// </value>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the note scope.
        /// </summary>
        /// <value>
        /// The note scope.
        /// </value>
        [JsonProperty("note_scope")]
        public string NoteScope { get; set; }

        /// <summary>
        /// Gets or sets the payment collector fee placement.
        /// </summary>
        /// <value>
        /// The payment collector fee placement.
        /// </value>
        [JsonProperty("payment_collector_fee_placement")]
        public string PaymentCollectorFeePlacement { get; set; }

        /// <summary>
        /// Gets or sets the payments.
        /// </summary>
        /// <value>
        /// The payments.
        /// </value>
        [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Used for deserializing vendor data.")]
        public JArray Payments { get; set; }

        /// <summary>
        /// Gets or sets the payment URL.
        /// </summary>
        /// <value>
        /// The payment URL.
        /// </value>
        [JsonProperty("payment_url")]
        public string PaymentUrl { get; set; }

        /// <summary>
        /// Gets or sets the receiver.
        /// </summary>
        /// <value>
        /// The receiver.
        /// </value>
        public string Receiver { get; set; }

        /// <summary>
        /// Gets or sets the sender email.
        /// </summary>
        /// <value>
        /// The sender email.
        /// </value>
        [JsonProperty("sender_email")]
        public string SenderEmail { get; set; }

        /// <summary>
        /// Gets or sets the sender mobile number.
        /// </summary>
        /// <value>
        /// The sender mobile number.
        /// </value>
        [JsonProperty("sender_mobile_number")]
        public string SenderMobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the sender.
        /// </summary>
        /// <value>
        /// The name of the sender.
        /// </value>
        [JsonProperty("sender_name")]
        public string SenderName { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the supported payment collectors.
        /// </summary>
        /// <value>
        /// The supported payment collectors.
        /// </value>
        [JsonProperty("supported_payment_collectors")]
        [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Used for deserializing vendor data.")]
        public List<string> SupportedPaymentCollectors { get; set; }

        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        /// <value>
        /// The updated at.
        /// </value>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}