using TesteAPI.Models;

namespace TesteAPI.Interfaces;

public interface IMainService
{
    Task<List<RepositoryModel>> GetOldestRepositoriesAsync(string endpoint);
}
