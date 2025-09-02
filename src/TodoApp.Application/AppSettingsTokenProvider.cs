using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace TodoApp
{
    public class AppSettingsTokenProvider : ITokenProvider
    {
        private readonly IConfiguration _configuration;
        public AppSettingsTokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<string> GetTokenAsync()
        {
            // Đọc token từ appsettings.json, ví dụ: "ThirdPartyApi:BearerToken"
            var token = _configuration["ThirdPartyApi:BearerToken"];
            return Task.FromResult(token);
        }
    }
}
