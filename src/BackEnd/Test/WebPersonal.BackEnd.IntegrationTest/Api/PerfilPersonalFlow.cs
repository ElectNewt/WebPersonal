using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebPersonal.BackEnd.API.Controllers;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories;
using WebPersonal.BackEnd.Service.PerfilPersonal;
using WebPersonal.BackEnd.ServiceDependencies.Services.PerfilPersonal;
using WebPersonal.Shared.Data.Db;
using WebPersonal.Shared.Dto;
using WebPersonal.Shared.ROP;
using Xunit;

namespace WebPersonal.BackEnd.IntegrationTest.Api
{
    public class PerfilPersonalFlow
    {

        [Fact]
        public async Task TestInsertPerfilPersonal_Then_ModifyIt()
        {
            IServiceCollection services = BuildDependencies();
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                string username = Guid.NewGuid().ToString();

                PersonalProfileDto defaultPRofile = BuildPersonalProfile(username);
                
                var departmentAppService = serviceProvider.GetRequiredService<PerfilPersonalController>();
                await departmentAppService.Post(defaultPRofile);

                ResultDto<PersonalProfileDto> resultUserStep1 = await departmentAppService.Get(username);
                Assert.Empty(resultUserStep1.Errors);
                PersonalProfileDto userStep1 = resultUserStep1.Value;
                Assert.Empty(userStep1.Skills);
                Assert.Equal(defaultPRofile.FirstName, userStep1.FirstName);
                Assert.Equal(defaultPRofile.Website, userStep1.Website);
                Assert.Equal(defaultPRofile.LastName, userStep1.LastName);

                SkillDto skill = new SkillDto()
                {
                    Id = null,
                    Name = "nombre1",
                    Punctuation = 10m
                };
                userStep1.Skills.Add(skill);

                InterestDto interest = new InterestDto()
                {
                    Id = null,
                    Interest = "interes pero debe contener 15 caracteres"
                };
                userStep1.Interests.Add(interest);
                var _ =await departmentAppService.Put(userStep1);
                //TODO: change back to get
                ResultDto<PersonalProfileDto> resultUserStep2 = await departmentAppService.Get(username);
                Assert.Empty(resultUserStep2.Errors);
                PersonalProfileDto userStep2 = resultUserStep1.Value;
                Assert.Single(userStep2.Skills);
                Assert.Equal(skill.Name, userStep2.Skills.First().Name);
                Assert.Single(userStep2.Interests);
                Assert.Equal(interest.Interest, userStep2.Interests.First().Interest);
                Assert.Equal(defaultPRofile.FirstName, userStep2.FirstName);
                Assert.Equal(defaultPRofile.Website, userStep2.Website);
                Assert.Equal(defaultPRofile.LastName, userStep2.LastName);
                Assert.Equal(defaultPRofile.Email, userStep2.Email);
            }
        }

        private PersonalProfileDto BuildPersonalProfile(string uniqueUsername)
        {
            return new PersonalProfileDto()
            {
                Description = "Description",
                Email = "email",
                FirstName = "firstName",
                GitHub = "github",
                Id = null,
                Interests = new List<InterestDto>(),
                LastName = "last name",
                Phone = "telefono",
                Skills = new List<SkillDto>(),
                UserId = null,
                UserName = uniqueUsername,
                Website = "web"
            };
        }



        private IServiceCollection BuildDependencies()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddScoped<DbConnection>(x
                => new MySqlConnection("Server=127.0.0.1;Port=4306;Database=webpersonal;Uid=root;password=test;Allow User Variables=True"))
                .AddScoped<TransactionalWrapper>()
                .AddScoped<PersonalProfile>()
                .AddScoped<PutPersonalProfile>()
                .AddScoped<PostPersonalProfile>()
                .AddScoped<IPostPersonalProfileDependencies, PostPersonalProfileDependencies>()
                .AddScoped<IGetPersonalProfileDependencies, GetPersonalProfileDependencies>()
                .AddScoped<IPutPersonalProfileDependencies, PutPersonalProfileDependencies>()
                .AddScoped<PersonalProfileRepository>()
                .AddScoped<SkillRepository>()
                .AddScoped<InterestsRepository>()
                .AddScoped<UserIdRepository>()
                .AddScoped<PerfilPersonalController>()
                .AddScoped< IDataProtectionProvider, EphemeralDataProtectionProvider>();

            return services;
        }
    }
}
