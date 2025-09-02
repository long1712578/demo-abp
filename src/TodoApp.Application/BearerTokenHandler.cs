using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp
{
    public class BearerTokenHandler : DelegatingHandler
    {
        private readonly ITokenProvider _tokenProvider;
        public BearerTokenHandler(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _tokenProvider.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
