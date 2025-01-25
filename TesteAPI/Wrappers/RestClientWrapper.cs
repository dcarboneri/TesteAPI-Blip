using TesteAPI.Interfaces;
using RestSharp;

namespace TesteAPI.Wrappers
{
    public class RestClientWrapper : IRestClientWrapper
    {
        private readonly RestClient _client;

        public RestClientWrapper(RestClient client)
        {
            _client = client;
        }

        public Task<RestResponse> ExecuteAsync(RestRequest request, CancellationToken cancellationToken = default)
        {
            return _client.ExecuteAsync(request, cancellationToken);
        }
    }
}
