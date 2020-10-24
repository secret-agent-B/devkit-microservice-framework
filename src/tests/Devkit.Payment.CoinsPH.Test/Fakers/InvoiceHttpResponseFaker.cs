// -----------------------------------------------------------------------
// <copyright file="InvoiceHttpResponseFaker.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Payment.CoinsPH.Test.Fakers
{
    using System;
    using System.Collections.Generic;
    using Bogus;
    using Devkit.Test;
    using Devkit.Payment.CoinsPH.Business.Invoice.Commands.CreateInvoice;

    /// <summary>
    /// The InvoiceFaker creates a fake CoinsPH response from create invoice request.
    /// </summary>
    public class InvoiceHttpResponseFaker : FakerBase<CreateInvoiceHttpResponse>
    {
        /// <summary>
        /// Generates the specified count.
        /// </summary>
        /// <returns>
        /// A list of T.
        /// </returns>
        public override CreateInvoiceHttpResponse Generate()
        {
            var faker = new Faker();
            var amount = Math.Round(faker.Random.Double(1.00, 100.00), 2, MidpointRounding.AwayFromZero);
            var rate = Math.Round(faker.Random.Double(1.00, 100.00), 2, MidpointRounding.AwayFromZero);
            var invoiceId = faker.Random.Hexadecimal(24, string.Empty);

            this.Faker
                .RuleFor(x => x.Amount, amount)
                .RuleFor(x => x.AmountDue, amount)
                .RuleFor(x => x.Category, "merchant")
                .RuleFor(x => x.CreatedAt, DateTime.Now)
                .RuleFor(x => x.Currency, f => f.Finance.Currency().Code)
                .RuleFor(x => x.ExpiresAt, DateTime.Now.AddMinutes(15))
                .RuleFor(x => x.ExpiresInSeconds, 800)
                .RuleFor(x => x.ExternalTransaction, f => f.Random.Hexadecimal(24, string.Empty))
                .RuleFor(x => x.Id, invoiceId)
                .RuleFor(x => x.IncomingAddress, f => f.Random.Hexadecimal(24, string.Empty))
                .RuleFor(x => x.InitialRate, rate)
                .RuleFor(x => x.LockedRate, rate)
                .RuleFor(x => x.NoteScope, "private")
                .RuleFor(x => x.PaymentCollectorFeePlacement, "top")
                .RuleFor(x => x.PaymentUrl, f => $"https://coins.ph/payment/invoice/" + invoiceId)
                .RuleFor(x => x.Receiver, f => f.Random.Hexadecimal(24, string.Empty))
                .RuleFor(x => x.Status, "pending")
                .RuleFor(x => x.SupportedPaymentCollectors, new List<string> { "cash_payment" })
                .RuleFor(x => x.UpdatedAt, DateTime.Now);

            return this.Faker.Generate();
        }
    }
}