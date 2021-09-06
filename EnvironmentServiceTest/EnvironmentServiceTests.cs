using EnvironmentService;
using EnvironmentService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
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
        public async Task Get_All_Existing_Environments()
        {
            
            //Arrange
            EnvironmentService.Models.Environment env = new EnvironmentService.Models.Environment { Name = "Town33", Number = 33, Segment = "DLO" };
            var postResponse = await _client.PostAsJsonAsync("/api/Environments", env);
            Assert.Equal(System.Net.HttpStatusCode.Created, postResponse.StatusCode);

            //Act
            var getResponse = await _client.GetAsync("/api/Environments");
            var resultJson = await getResponse.Content.ReadFromJsonAsync<Environment>();


            //var getResponseString = await getResponse.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, getResponse.StatusCode);
            //Assert.Equal(env.Name, resultJson.Name);

        }


        [Fact]
        public async Task Get_Environment_By_Id()
        {

            //Arrange
            //Create Environment

            //Act
            var getResponse = await _client.GetAsync("/api/Environments/1");
            var fetchedEnvironment = await getResponse.Content.ReadFromJsonAsync<Environment>();

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, getResponse.StatusCode);
            Assert.Equal(1, fetchedEnvironment.Id);
            Assert.Equal("Town33", fetchedEnvironment.Name);
            Assert.Equal(33, fetchedEnvironment.Number);
            Assert.Equal("DLO", fetchedEnvironment.Segment);
        }

        [Fact]
        public async Task Get_Environment_By_Id_Not_Found()
        {

            //Arrange
            //Create Environment

            //Act
            var getResponse = await _client.GetAsync("/api/Environments/100");

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, getResponse.StatusCode);
        }

        [Fact]
        public async Task Get_Environment_By_Id_Bad_Request()
        {

            //Arrange
            //Create Environment

            //Act
            var getResponse = await _client.GetAsync("/api/Environments/bad-request");

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, getResponse.StatusCode);
        }

    }
}
