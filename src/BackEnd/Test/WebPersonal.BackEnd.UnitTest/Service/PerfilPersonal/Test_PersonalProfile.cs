using Microsoft.AspNetCore.DataProtection;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Service.PerfilPersonal;
using Xunit;

namespace WebPersonal.BackEnd.UnitTest.Service.PerfilPersonal
{
    public class Test_PersonalProfile
    {

        private class TestState
        {
            public Mock<IGetPersonalProfileDependencies> _dependencies;
            public PersonalProfile Subject;
            public string Username = "test";
            public int UserId = 123;

            public TestState()
            {
                Mock<IGetPersonalProfileDependencies> dependencies = new Mock<IGetPersonalProfileDependencies>();

                IDataProtectionProvider provider = new EphemeralDataProtectionProvider();

                dependencies.Setup(a => a.GetUserId(Username))
                .Returns(Task.FromResult(UserIdEntity.Create(Username, UserId)));

                dependencies.Setup(a => a.GetPersonalProfile(UserId))
                    .Returns(Task.FromResult(GetPersonalProfileEntity(UserId)));

                dependencies.Setup(a => a.GetSkills(UserId))
                    .Returns(Task.FromResult(GetSkills(UserId)));

                dependencies.Setup(a => a.GetInterests(UserId))
                    .Returns(Task.FromResult(GetInterests(UserId)));

                _dependencies = dependencies;

                Subject = new PersonalProfile(_dependencies.Object, provider);

            }

            private PersonalProfileEntity GetPersonalProfileEntity(int UserId)
            {
                return PersonalProfileEntity.Create(UserId, 1, "firstName", "LastName", "Descripción", "Telefono", "email@email.com",
                    "http://www.netmentor.es", "/ElectNewt");
            }

            private List<SkillEntity> GetSkills(int UserId)
            {
                return new List<SkillEntity>()
                {
                    SkillEntity.Create(UserId, 1, "skill1", 10),
                    SkillEntity.Create(UserId, 2, "skill2", null)
                };
            }

            private List<InterestEntity> GetInterests(int userId)
            {
                return new List<InterestEntity>()
                {
                    InterestEntity.Create(1, userId, "interest1"),
                    InterestEntity.Create(2, userId, "interest2")
                };
            }

        }

        [Fact]
        public async Task Test_allCorrect_ThenSuccess()
        {
            var state = new TestState();

            var result = await state.Subject.GetPersonalProfileDto(state.Username);

            Assert.True(result.Success);
            Assert.Equal("firstName", result.Value.FirstName);
            Assert.Equal(2, result.Value.Skills.Count);
            Assert.Equal("skill1", result.Value.Skills.First().Name);
            Assert.Equal(2, result.Value.Interests.Count);
            Assert.Equal("interest1", result.Value.Interests.First().Interest);

        }



        [Fact]
        public async Task Test_User_NonExistent_ThenError()
        {

            var state = new TestState();

            state._dependencies.Setup(a => a.GetUserId(It.IsAny<string>()))
                .Returns(Task.FromResult(null as UserIdEntity));

            var result = await state.Subject.GetPersonalProfileDto("noExistent");

            Assert.False(result.Success);
            Assert.Single(result.Errors);
            Assert.Equal("UserIdentity no encontrado", result.Errors.First().Message);
        }


        [Fact]
        public async Task Test_Profile_NonExistent_ThenError()
        {

            var state = new TestState();

            state._dependencies.Setup(a => a.GetPersonalProfile(It.IsAny<int>()))
                .Returns(Task.FromResult(null as PersonalProfileEntity));

            var result = await state.Subject.GetPersonalProfileDto(state.Username);

            Assert.False(result.Success);
            Assert.Single(result.Errors);
            Assert.Equal("personal profile no encontrado", result.Errors.First().Message);
        }

    }
}
