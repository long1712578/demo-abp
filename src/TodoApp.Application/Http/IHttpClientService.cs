using System.Net.Http;
using System.Threading.Tasks;

namespace TodoApp.Application.Http
{
    public interface IHttpClientService
    {
        Task<string> GetAsync(string apiName, string relativeUrl);
        Task<string> PostAsync(string apiName, string relativeUrl, HttpContent content);
    }
}
