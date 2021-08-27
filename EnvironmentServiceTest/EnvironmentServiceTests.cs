using EnvironmentService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace EnvironmentServiceTest
{
    public class EnvironmentServiceTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public EnvironmentServiceTests()
        {
            //Arrange
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
            //Clear DB
        }

        [Fact]
        public async Task Get_Environments_When_None_Exists_Should_Return_Empty_List()
        {
            //Act
            var response = await _client.GetAsync("/api/Environments");
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("[]", responseString);

        }

        [Fact]
        public async Task Get_Environments_When_Atleast_One_Environment_Exists_Should_Return_Environments()
        {
            EnvironmentService.Models.Environment env = new EnvironmentService.Models.Environment { Name = "Town33", Number = 33, Segment = "DLI" }; 
            
            //Act
            var response = await _client.PostAsJsonAsync("/api/Environments", env);
            //Assert
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
            
        }
    }
}
