// -----------------------------------------------------------------------
// <copyright file="TestHttpResponse.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Test
{
    using System.Net;

    /// <summary>
    /// HttpClient response from the API.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public class TestHttpResponse<TResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestHttpResponse{TResponse}" /> class.
        /// </summary>
        /// <param name="apiResponse">The API response.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="isSuccessfulStatusCode">if set to <c>true</c> [is successful status code].</param>
        internal TestHttpResponse(TResponse apiResponse, HttpStatusCode statusCode, bool isSuccessfulStatusCode)
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