// -----------------------------------------------------------------------
// <copyright file="CreateInvoiceHandler.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Payment.CoinsPH.Business.Invoice.Commands.CreateInvoice
{
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.Http.Extensions;
    using Devkit.Patterns.CQRS.Command;
    using Devkit.Payment.CoinsPH;
    using Devkit.Payment.ViewModels;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// The CreateInvoiceHandler handles the CreateInvoiceCommand.
    /// </summary>
    public class CreateInvoiceHandler : CommandHandlerBase<CreateInvoiceCommand, VendorTransactionVM>
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly CoinsPHConfiguration _configuration;

        /// <summary>
        /// The HTTP client.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInvoiceHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <param name="options">The options.</param>
        public CreateInvoiceHandler(IRepository repository, IHttpClientFactory httpClientFactory, [NotNull] IOptions<CoinsPHConfiguration> options)
            : base(repository)
        {
            this._httpClient = httpClientFactory.CreateClient();
            this._configuration = options.Value;
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = await this._httpClient.PostAsync<CreateInvoiceHttpResponse>(this._configuration.InvoiceRoute, new
            {
                this.Request.Amount,
                this.Request.Currency,
                this.Request.TransactionId
            });

            this.Response.Amount = response.Payload.Amount;
            this.Response.AmountDue = response.Payload.AmountDue;
            this.Response.Currency = response.Payload.Currency;
            this.Response.InvoiceId = response.Payload.Id;
            this.Response.TransactionId = response.Payload.ExternalTransaction;
            this.Response.PaymentUrl = response.Payload.PaymentUrl;
            this.Response.CreatedAt = response.Payload.CreatedAt;
            this.Response.UpdatedAt = response.Payload.UpdatedAt;
            this.Response.ExpiresAt = response.Payload.ExpiresAt;
        }
    }
}