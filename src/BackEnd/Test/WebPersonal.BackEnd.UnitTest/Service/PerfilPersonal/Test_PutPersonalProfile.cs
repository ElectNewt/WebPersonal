using Microsoft.AspNetCore.DataProtection;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ROP;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Service.Mappers;
using WebPersonal.BackEnd.Service.PerfilPersonal;
using WebPersonal.Shared.Dto;
using Xunit;

namespace WebPersonal.BackEnd.UnitTest.Service.PerfilPersonal
{
    public class Test_PutPersonalProfile
    {
        public class TestState
        {
            public Mock<IPutPersonalProfileDependencies> _dependencies;
            public PutPersonalProfile Subject;
            public string Username = "NombreUsuario";
            public int UserId = 123;
            public readonly PersonalProfileDto DefaultPersonalProfile;

            public TestState()
            {
                DefaultPersonalProfile = BuildPersonalProfile();
                IDataProtectionProvider provider = new EphemeralDataProtectionProvider();
                IDataProtector protector = provider.CreateProtector("test");

                var entities = DefaultPersonalProfile.MapToWraperEntities(protector);

                Mock<IPutPersonalProfileDependencies> dependencies = new Mock<IPutPersonalProfileDependencies>();
                //TODO: modify the scenario to test as well updates.
                dependencies.Setup(a => a.InsertPersonalProfile(It.IsAny<PersonalProfileEntity>()))
                    .ReturnsAsync(entities.personalProfile);

                dependencies.Setup(a => a.UpdatePersonalProfile(It.IsAny<PersonalProfileEntity>()))
                    .ReturnsAsync(entities.personalProfile);

                dependencies.Setup(a => a.InsertInterests(It.IsAny<List<InterestEntity>>()))
                    .ReturnsAsync(entities.interestEntities);

                dependencies.Setup(a => a.UpdateInterests(It.IsAny<List<InterestEntity>>()))
                    .ReturnsAsync(entities.interestEntities);

                dependencies.Setup(a => a.InsertSkills(It.IsAny<List<SkillEntity>>()))
                    .ReturnsAsync(entities.skillEntities);

                dependencies.Setup(a => a.UpdateSkills(It.IsAny<List<SkillEntity>>()))
                    .ReturnsAsync(entities.skillEntities);

                dependencies.Setup(a => a.CommitTransaction())
                    .Returns(Task.CompletedTask);

                dependencies.Setup(a => a.GetUser(Username))
                    .ReturnsAsync(UserIdEntity.Create(Username, UserId));

                _dependencies = dependencies;

                Subject = new PutPersonalProfile(_dependencies.Object, provider);
            }

            private PersonalProfileDto BuildPersonalProfile()
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
                            Id = null,
                            Interest = "interest 1 es un test algo largo"
                        }
                    },
                    LastName = "last name",
                    Phone = "telefono",
                    Skills = new List<SkillDto>()
                    {
                        new SkillDto()
                        {
                            Id = null,
                            Name = "skill1",
                            Punctuation = null
                        }
                    },
                    UserId = this.UserId,
                    UserName = this.Username,
                    Website = "web"
                };
            }
        }


        [Fact]
        public async Task Test_CorrectUserAndSystem_ThenCorrectInformation()
        {
            var state = new TestState();

            var result = await state.Subject.Create(state.DefaultPersonalProfile);

            Assert.True(result.Success);
            Assert.Equal("firstName", result.Value.FirstName);
            Assert.Single(result.Value.Skills);
            Assert.Single(result.Value.Interests);
        }
    }
}