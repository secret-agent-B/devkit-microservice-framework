// -----------------------------------------------------------------------
// <copyright file="IntegrationTestBase.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Test
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Devkit.Data;
    using Newtonsoft.Json;
    using Xunit;

    /// <summary>
    /// The base class that helps facilitate integration tests.
    /// </summary>
    /// <typeparam name="T">The type of input that is being tested.</typeparam>
    /// <typeparam name="TStartup">The type of the startup.</typeparam>
    /// <seealso cref="TestBase{T}" />
    [Trait("Category", "Integration Test")]
    public abstract class IntegrationTestBase<T, TStartup> : TestBase<T>, IClassFixture<AppTestFixture<TStartup>>
        where TStartup : class
    {
        /// <summary>
        /// The application test fixture.
        /// </summary>
        private readonly AppTestFixture<TStartup> _appTestFixture;

        /// <summary>
        /// The HTTP client.
        /// </summary>
        private HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationTestBase{T, TStartup}"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        protected IntegrationTestBase(AppTestFixture<TStartup> testFixture)
            : base()
        {
            this.Repository = new Repository(new RepositoryOptions
            {
                ConnectionString = testFixture?.RepositoryConfiguration.ConnectionString,
                DatabaseName = testFixture.RepositoryConfiguration.DatabaseName
            });

            this.SeedDatabase();

            this._appTestFixture = testFixture;
        }

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        protected HttpClient Client
        {
            get { return this._httpClient ??= this._appTestFixture.Server.CreateClient(); }
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="route">The URI.</param>
        /// <returns>
        /// An http response message.
        /// </returns>
        protected async Task<TResponse> DeleteAsync<TResponse>(string route)
        {
            var response = await this.Client.DeleteAsync(this.ConvertRouteToUri(route));
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(json);
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="route">The URI.</param>
        /// <returns>
        /// An http response message.
        /// </returns>
        protected async Task<TestHttpResponse<TResponse>> GetAsync<TResponse>(string route)
        {
            var response = await this.Client.GetAsync(this.ConvertRouteToUri(route), HttpCompletionOption.ResponseContentRead);
            var json = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<TResponse>(json);
            return new TestHttpResponse<TResponse>(apiResponse, response.StatusCode, response.IsSuccessStatusCode);
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="route">The URI.</param>
        /// <param name="content">The content.</param>
        /// <param name="throwIfNotSuccessful">if set to <c>true</c> [throw if not successful].</param>
        /// <returns>
        /// An http response message.
        /// </returns>
        protected async Task<TestHttpResponse<TResponse>> PostAsync<TResponse>(string route, object content, bool throwIfNotSuccessful = false)
        {
            using var stringContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            var response = await this.Client.PostAsync(this.ConvertRouteToUri(route), stringContent);

            if (!response.IsSuccessStatusCode && throwIfNotSuccessful)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(responseString);
            }

            var json = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<TResponse>(json);
            return new TestHttpResponse<TResponse>(apiResponse, response.StatusCode, response.IsSuccessStatusCode);
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="route">The URI.</param>
        /// <returns>
        /// An http response message.
        /// </returns>
        protected async Task<TestHttpResponse<TResponse>> PostAsync<TResponse>(string route)
        {
            using var stringContent = new StringContent(JsonConvert.SerializeObject(new { }), Encoding.UTF8, "application/json");

            var response = await this.Client.PostAsync(this.ConvertRouteToUri(route), stringContent);
            var json = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<TResponse>(json);
            return new TestHttpResponse<TResponse>(apiResponse, response.StatusCode, response.IsSuccessStatusCode);
        }

        /// <summary>
        /// Updates a record through an API call.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="route">The URI.</param>
        /// <param name="content">The content.</param>
        /// <returns>
        /// An http response message.
        /// </returns>
        protected async Task<TestHttpResponse<TResponse>> PutAsync<TResponse>(string route, object content)
        {
            using var stringContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            var response = await this.Client.PutAsync(this.ConvertRouteToUri(route), stringContent);
            var json = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<TResponse>(json);
            return new TestHttpResponse<TResponse>(apiResponse, response.StatusCode, response.IsSuccessStatusCode);
        }

        /// <summary>
        /// Converts the route to URI.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>A Uri version of the route.</returns>
        private Uri ConvertRouteToUri(string route)
        {
            if (!string.IsNullOrEmpty(route) && route.StartsWith("/", StringComparison.InvariantCulture))
            {
                route = route.Substring(1);
            }

            var url = $"{this.Client.BaseAddress}{route}";

            return new Uri(url);
        }
    }
}