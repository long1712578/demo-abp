using System.Threading.Tasks;

namespace TodoApp
{
    public interface ITokenProvider
    {
        Task<string> GetTokenAsync();
    }
}
