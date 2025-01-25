using RestSharp;

public interface IRestClientWrapper
{
    Task<RestResponse> ExecuteAsync(RestRequest request, CancellationToken cancellationToken = default);
}
