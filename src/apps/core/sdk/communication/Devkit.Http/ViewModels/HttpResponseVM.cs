// -----------------------------------------------------------------------
// <copyright file="HttpResponseVM.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Http.ViewModels
{
    using System.Net;

    /// <summary>
    /// The HttpResponseVM is a wrapper for the HTTPResponse that is sent back from an HTTPClient call.
    /// </summary>
    public class HttpResponseVM<TResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseVM{TResponse}" /> class.
        /// </summary>
        /// <param name="apiResponse">The API response.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="isSuccessfulStatusCode">if set to <c>true</c> [is successful status code].</param>
        internal HttpResponseVM(TResponse apiResponse, HttpStatusCode statusCode, bool isSuccessfulStatusCode)
        {
            this.Payload = apiResponse;
            this.StatusCode = statusCode;
            this.IsSuccessfulStatusCode = isSuccessfulStatusCode;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is successful status code.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is successful status code; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccessfulStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public TResponse Payload { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode StatusCode { get; set; }
    }
}