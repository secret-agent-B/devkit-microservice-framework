// -----------------------------------------------------------------------
// <copyright file="CreateInvoiceCommandFaker.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Payment.CoinsPH.Test.Business.Invoice.Commands
{
    using System;
    using Devkit.Test;
    using Devkit.Payment.CoinsPH.Business.Invoice.Commands.CreateInvoice;

    /// <summary>
    /// The CreateInvoiceCommandFaker fakes the CreateInvoiceCommand.
    /// </summary>
    public class CreateInvoiceCommandFaker : FakerBase<CreateInvoiceCommand>
    {
        /// <summary>
        /// Generates the specified count.
        /// </summary>
        /// <returns>
        /// A list of T.
        /// </returns>
        public override CreateInvoiceCommand Generate()
        {
            this.Faker
                .RuleFor(x => x.Amount, f => Math.Round(f.Random.Double(1.00, 100.00), 2, MidpointRounding.AwayFromZero))
                .RuleFor(x => x.Currency, f => f.Finance.Currency().Code)
                .RuleFor(x => x.TransactionId, f => f.Random.Hexadecimal(24, string.Empty));

            return this.Faker.Generate();
        }
    }
}