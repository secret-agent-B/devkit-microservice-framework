// -----------------------------------------------------------------------
// <copyright file="CreateInvoiceConsumer.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Payment.PayMaya.ServiceBus.Consumers.CreateInvoice
{
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Devkit.Communication.Payment.DTOs;
    using Devkit.Communication.Payment.Messages;
    using Devkit.Payment.PayMaya.Configurations;
    using Devkit.ServiceBus;
    using Devkit.Http.Extensions;
    using MassTransit;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// The CreateInvoiceConsumer consumes the create invoice for PayMaya payments.
    /// </summary>
    public class CreateInvoiceConsumer : MessageConsumerBase<ICreateInvoice, IInvoiceDTO>
    {
        /// <summary>
        /// The configiration object.
        /// </summary>
        private readonly PayMayaConfiguration _configuration;

        /// <summary>
        /// The HTTP client.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInvoiceConsumer" /> class.
        /// </summary>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <param name="options">The options.</param>
        public CreateInvoiceConsumer(IHttpClientFactory httpClientFactory, [NotNull] IOptions<PayMayaConfiguration> options)
        {
            this._httpClient = httpClientFactory.CreateClient();
            this._configuration = options.Value;
        }

        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected override async Task ConsumeMessage(ConsumeContext<ICreateInvoice> context)
        {
            _ = await this._httpClient.PostAsync<CreateInvoiceHttpResponse>(this._configuration.BaseUrl, new
            {
                invoice = context.Message.TransactionId,
                type = "SINGLE",
                totalAmount = new
                {
                    value = context.Message.Amount,
                    currency = context.Message.Currency
                },
                redirectUrl = new
                {
                    success = this._configuration.SuccessUrl,
                    failure = this._configuration.FailureUrl,
                    cancel = this._configuration.CancelUrl
                },
                requestReferenceNumber = context.RequestId
            });
        }
    }
}