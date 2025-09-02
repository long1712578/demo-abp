using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace TodoApp.Application.Http
{
    public class SevagoApiClient : ISevagoApiClient, ITransientDependency
    {
        private readonly IHttpClientService _httpClientService;
        private const string ApiName = "Sevago";

        public SevagoApiClient(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<string> GetUserProfileAsync(string userId)
        {
            // Ví dụ endpoint: /users/{userId}/profile
            var relativeUrl = $"users/{userId}/profile";
            return await _httpClientService.GetAsync(ApiName, relativeUrl);
        }

        // Thêm các method đặc thù cho Sevago API ở đây
    }
}
