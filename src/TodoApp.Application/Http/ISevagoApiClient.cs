using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace TodoApp.Application.Http
{
    public interface ISevagoApiClient : ITransientDependency
    {
        Task<string> GetUserProfileAsync(string userId);
        // Thêm các method đặc thù cho Sevago API ở đây
    }
}
