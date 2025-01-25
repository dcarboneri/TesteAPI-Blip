using Newtonsoft.Json;
using RestSharp.Authenticators;
using RestSharp;
using TesteAPI.Models;
using TesteAPI.Interfaces;
using TesteAPI.Wrappers;

namespace TesteAPI.Services
{
    public class MainService : IMainService
    {
        private readonly IRestClientWrapper _client;

        public MainService(string token)
        {
            var restClient = new RestClient(new RestClientOptions("https://api.github.com")
            {
                Authenticator = new JwtAuthenticator(token)
            });

            _client = new RestClientWrapper(restClient);
        }

        public MainService(IRestClientWrapper client)
        {
            _client = client;
        }

        public async Task<List<RepositoryModel>> GetOldestRepositoriesAsync(string organization)
        {
            try
            {
                RestRequest request = new RestRequest($"orgs/{organization}/repos", Method.Get);
                RestResponse response = await _client.ExecuteAsync(request);

                List<RepositoryModel>? allRepos = JsonConvert.DeserializeObject<List<RepositoryModel>>(response.Content);
                List<RepositoryModel> csharpRepos = allRepos.OrderBy(repo => repo.Created_At).Where(repo => repo.Language == "C#").Take(5).ToList();

                return csharpRepos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
