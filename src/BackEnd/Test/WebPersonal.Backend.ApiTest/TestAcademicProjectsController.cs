using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using WebPersonal.BackEnd.API;
using WebPersonal.Shared.Dto;
using Xunit;

namespace WebPersonal.Backend.ApiTest;

public class TestAcademicProjectsController
{
    [Fact]
    public async Task WhenCallAPI_withID1_thenResult()
    {
        IWebHostBuilder webHostBuilder =
            new WebHostBuilder()
                .ConfigureAppConfiguration(x => x.AddJsonFile("appsettings.tests.json", optional: true))
                .UseEnvironment("production")
                .UseStartup<Startup>();
        
        using (TestServer server = new TestServer(webHostBuilder))
        using (HttpClient client = server.CreateClient())
        {
            AcademicProjectsDto result = await client.GetFromJsonAsync<AcademicProjectsDto>("/api/AcademicProjects/1",
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            Assert.Equal(1, result.Projects.First().Id);
        }
    }
}