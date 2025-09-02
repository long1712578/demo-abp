using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web;
using Volo.Abp.DependencyInjection;

namespace TodoApp.Application.Http
{
    public class HttpClientService : IHttpClientService, ITransientDependency
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public HttpClientService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        /// <summary>
        /// GET trả về string (dữ liệu JSON hoặc text)
        /// </summary>
        public async Task<string> GetAsync(string apiName, string relativeUrl)
        {
            var (client, baseUrl) = CreateClient(apiName);
            var response = await client.GetAsync(baseUrl + relativeUrl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// POST trả về string (dữ liệu JSON hoặc text)
        /// </summary>
        public async Task<string> PostAsync(string apiName, string relativeUrl, HttpContent content)
        {
            var (client, baseUrl) = CreateClient(apiName);
            var response = await client.PostAsync(baseUrl + relativeUrl, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// GET trả về HttpResponseMessage (nếu muốn tự xử lý status code, header...)
        /// </summary>
        public async Task<HttpResponseMessage> GetRawAsync(string apiName, string relativeUrl)
        {
            var (client, baseUrl) = CreateClient(apiName);
            return await client.GetAsync(baseUrl + relativeUrl);
        }

        /// <summary>
        /// GET trả về object (deserialize JSON)
        /// </summary>
        public async Task<T> GetAsObjectAsync<T>(string apiName, string relativeUrl)
        {
            var (client, baseUrl) = CreateClient(apiName);
            var response = await client.GetAsync(baseUrl + relativeUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(json);
        }

        /// <summary>
        /// GET với query string động
        /// </summary>
        public async Task<string> GetWithQueryAsync(string apiName, string relativeUrl, IDictionary<string, string> queryParams)
        {
            var (client, baseUrl) = CreateClient(apiName);
            var uriBuilder = new System.UriBuilder(baseUrl + relativeUrl);
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
            foreach (var kv in queryParams)
            {
                query[kv.Key] = kv.Value;
            }
            uriBuilder.Query = query.ToString();
            var response = await client.GetAsync(uriBuilder.ToString());
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        private (HttpClient, string) CreateClient(string apiName)
        {
            var baseUrl = _configuration[$"Apis:{apiName}:BaseUrl"];
            var token = _configuration[$"Apis:{apiName}:BearerToken"];
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return (client, baseUrl);
        }
    }
}
