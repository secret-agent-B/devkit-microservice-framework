// -----------------------------------------------------------------------
// <copyright file="HttpClientExtensions.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Http.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Devkit.Http.ViewModels;
    using Newtonsoft.Json;

    /// <summary>
    /// The HttpExtension is the static class that contains helper methods for HTTPClient.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="route">The URI.</param>
        /// <returns>
        /// An http response message.
        /// </returns>
        public static async Task<HttpResponseVM<TResponse>> DeleteAsync<TResponse>([NotNull] this HttpClient httpClient, string route)
        {
            var response = await httpClient.DeleteAsync(ConvertRouteToUri(httpClient.BaseAddress, route));
            var json = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<TResponse>(json);

            return new HttpResponseVM<TResponse>(apiResponse, response.StatusCode, response.IsSuccessStatusCode);
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="route">The URI.</param>
        /// <returns>
        /// An http response message.
        /// </returns>
        public static async Task<HttpResponseVM<TResponse>> GetAsync<TResponse>([NotNull] this HttpClient httpClient, string route)
        {
            var response = await httpClient.GetAsync(ConvertRouteToUri(httpClient.BaseAddress, route), HttpCompletionOption.ResponseContentRead);
            var json = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<TResponse>(json);

            return new HttpResponseVM<TResponse>(apiResponse, response.StatusCode, response.IsSuccessStatusCode);
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="route">The URI.</param>
        /// <param name="content">The content.</param>
        /// <param name="throwIfNotSuccessful">if set to <c>true</c> [throw if not successful].</param>
        /// <returns>
        /// An http response message.
        /// </returns>
        /// <exception cref="HttpRequestException"></exception>
        public static async Task<HttpResponseVM<TResponse>> PostAsync<TResponse>([NotNull] this HttpClient httpClient, string route, object content, bool throwIfNotSuccessful = false)
        {
            using var stringContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(ConvertRouteToUri(httpClient.BaseAddress, route), stringContent);

            if (!response.IsSuccessStatusCode && throwIfNotSuccessful)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(responseString);
            }

            var json = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<TResponse>(json);

            return new HttpResponseVM<TResponse>(apiResponse, response.StatusCode, response.IsSuccessStatusCode);
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="route">The URI.</param>
        /// <returns>
        /// An http response message.
        /// </returns>
        public static async Task<HttpResponseVM<TResponse>> PostAsync<TResponse>([NotNull] this HttpClient httpClient, string route)
        {
            using var stringContent = new StringContent(JsonConvert.SerializeObject(new { }), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(ConvertRouteToUri(httpClient.BaseAddress, route), stringContent);
            var json = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<TResponse>(json);

            return new HttpResponseVM<TResponse>(apiResponse, response.StatusCode, response.IsSuccessStatusCode);
        }

        /// <summary>
        /// Updates a record through an API call.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="route">The URI.</param>
        /// <param name="content">The content.</param>
        /// <returns>
        /// An http response message.
        /// </returns>
        public static async Task<HttpResponseVM<TResponse>> PutAsync<TResponse>([NotNull] this HttpClient httpClient, string route, object content)
        {
            using var stringContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(ConvertRouteToUri(httpClient.BaseAddress, route), stringContent);
            var json = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<TResponse>(json);

            return new HttpResponseVM<TResponse>(apiResponse, response.StatusCode, response.IsSuccessStatusCode);
        }

        /// <summary>
        /// Converts the route to URI.
        /// </summary>
        /// <param name="baseAddress">The base address.</param>
        /// <param name="route">The route.</param>
        /// <returns>
        /// A Uri version of the route.
        /// </returns>
        private static Uri ConvertRouteToUri(Uri baseAddress, string route)
        {
            if (!string.IsNullOrEmpty(route) && route.StartsWith("/", StringComparison.InvariantCulture))
            {
                route = route.Substring(1);
            }

            var url = $"{baseAddress}{route}";

            return new Uri(url);
        }
    }
}