using Xunit;
using Moq;
using RestSharp;
using TesteAPI.Models;
using TesteAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TesteAPI.Interfaces;

namespace TesteAPI.Tests
{
    public class MainServiceTests
    {
        [Fact]
        public async Task GetOldestRepositoriesAsync_ReturnsOldestCSharpRepositories()
        {
            // Arrange
            var mockClientWrapper = new Mock<IRestClientWrapper>();

            var repositories = new List<RepositoryModel>
            {
                new RepositoryModel { Created_At = "2020, 1, 1", Language = "C#" },
                new RepositoryModel { Created_At = "2021, 1, 1", Language = "JavaScript" },
                new RepositoryModel { Created_At = "2019, 1, 1", Language = "C#" },
                new RepositoryModel { Created_At = "2022, 1, 1", Language = "C#" },
                new RepositoryModel { Created_At = "2018, 1, 1", Language = "C#" },
                new RepositoryModel { Created_At = "2017, 1, 1", Language = "C#" }
            };

            var restResponse = new RestResponse
            {
                Content = JsonConvert.SerializeObject(repositories),
                StatusCode = System.Net.HttpStatusCode.OK
            };

            mockClientWrapper.Setup(c => c.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(restResponse);

            var service = new MainService(mockClientWrapper.Object);

            // Act
            var result = await service.GetOldestRepositoriesAsync("fake_organization");

            // Assert
            Assert.Equal(5, result.Count);
            Assert.Equal("2017, 1, 1", result.First().Created_At);
        }
    }
}
