using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebPersonal.BackEnd.API;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Service.PerfilPersonal;
using WebPersonal.Shared.Dto;
using WebPersonal.Shared.ROP;
using Xunit;

namespace WebPersonal.Backend.ApiTest;

public class TestPerfilPersonalController
{
    [Fact]
    public async Task WhenInsertInformation_returnCorrect()
    {
        IWebHostBuilder webHostBuilder =
            new WebHostBuilder()
                .ConfigureTestServices(serviceCollection =>
                {
                    serviceCollection
                        .AddScoped<IPostPersonalProfileDependencies, StubIPostPersonalProfileDependencies>();
                })
                .UseStartup<Startup>();
        
        PersonalProfileDto defaultPersonalProfileDto = GetPersonalProfile();
        string serializedProfile = JsonSerializer.Serialize(defaultPersonalProfileDto);

        using (TestServer server = new TestServer(webHostBuilder))
        using (HttpClient client = server.CreateClient())
        {
            var result = await client.PostAsync("/api/PerfilPersonal/returnonlyid",
                new StringContent(serializedProfile, Encoding.UTF8, "application/json"));

            result.EnsureSuccessStatusCode();
        }
    }


    public PersonalProfileDto GetPersonalProfile()
    {
        return new PersonalProfileDto()
        {
            Description = "Description",
            Email = "email",
            FirstName = "firstName",
            GitHub = "github",
            Id = null,
            Interests = new List<InterestDto>()
            {
                new InterestDto()
                {
                    Id = 1,
                    Interest = "interest 1 es un test algo largo"
                }
            },
            LastName = "last name",
            Phone = "telefono",
            Skills = new List<SkillDto>()
            {
                new SkillDto()
                {
                    Id = 2,
                    Name = "skill1",
                    Punctuation = null
                }
            },
            UserName = "username",
            Website = "web"
        };
    }

    public class StubIPostPersonalProfileDependencies : IPostPersonalProfileDependencies
    {
        public Task<UserIdEntity> InsertUserId(string name)
            => Task.FromResult(UserIdEntity.Create(name, 1));

        public Task<Result<PersonalProfileEntity>> InsertPersonalProfile(PersonalProfileEntity personalProfile)
            => PersonalProfileEntity.Create(personalProfile.UserId, personalProfile.Id, personalProfile.FirstName,
                personalProfile.LastName, personalProfile.Description, personalProfile.Phone,
                personalProfile.Email, personalProfile.Website, personalProfile.GitHub).Success().Async();

        public Task<Result<List<SkillEntity>>> InsertSkills(List<SkillEntity> skills)
            => skills.Select(a => SkillEntity.Create(a.UserId, a.Id, a.Name, a.Punctuation)).ToList().Success().Async();

        public Task<Result<List<InterestEntity>>> InsertInterests(List<InterestEntity> interests)
            => interests.Select(a => InterestEntity.Create(a.Id, a.UserId, a.Description)).ToList().Success().Async();

        public Task<Result<bool>> SendEmail(string to, string subject, string body)
            => true.Success().Async();

        public Task CommitTransaction()
            => Task.CompletedTask;
    }
    
 
}