using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Service.Mappers;
using WebPersonal.BackEnd.Service.PerfilPersonal;
using WebPersonal.Shared.Dto;
using WebPersonal.Shared.ROP;
using Xunit;

namespace WebPersonal.BackEnd.UnitTest.Service.PerfilPersonal
{
    public class Test_PostPersonalProfile
    {
        public class TestState
        {
            public Mock<IPostPersonalProfileDependencies> _dependencies;
            public PostPersonalProfile Subject;
            public string Username = "NombreUsuario";
            public int UserId = 123;
            public readonly PersonalProfileDto DefaultPersonalProfile;

            public TestState()
            {
                DefaultPersonalProfile = BuildPersonalProfile();

                var entities = DefaultPersonalProfile.MapToEntities();

                Mock<IPostPersonalProfileDependencies> dependencies = new Mock<IPostPersonalProfileDependencies>();

                dependencies.Setup(a => a.SavePersonalProfile(It.IsAny<PersonalProfileEntity>()))
                    .Returns(entities.Item1.Success().Async());

                dependencies.Setup(a => a.SaveInterests(It.IsAny<List<InterestEntity>>()))
                    .Returns(entities.Item3.Success().Async());

                dependencies.Setup(a => a.SaveSkills(It.IsAny<List<SkillEntity>>()))
                    .Returns(entities.Item2.Success().Async());

                dependencies.Setup(a => a.CommitTransaction())
                    .Returns(Task.CompletedTask);

                dependencies.Setup(a => a.GetUser(Username))
                    .Returns(Task.FromResult(UserIdEntity.Create(Username, UserId)));

                _dependencies = dependencies;

                Subject = new PostPersonalProfile(_dependencies.Object);

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
