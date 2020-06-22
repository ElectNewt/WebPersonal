using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.Shared.Dto;

namespace WebPersonal.BackEnd.Model.Services
{
    public interface IPersonalProfileServiceDependencies
    {
        Task<UserIdEntity> GetUserId(string name);
        Task<List<InterestEntity>> GetInterests(int userId);
        Task<List<SkillEntity>> GetSkills(int id);
        Task<PersonalProfileEntity> GetPersonalProfile(int id);
    }

    public class PersonalProfileService
    {
        private readonly IPersonalProfileServiceDependencies _dependencies;

        public PersonalProfileService(IPersonalProfileServiceDependencies dependencies)
        {
            _dependencies = dependencies;
        }


        public async Task<PersonalProfileDto> GetPersonalProfileDto(string name)
        {

            UserIdEntity userId = await GetUserId(name);

            var interests = GetInterests(userId.UserId);
            var skills = GetSkills(userId.UserId);
            var personalProfile = GetPersonalProfile(userId.UserId);
            _ = Task.WhenAll(
                interests,
                skills,
                personalProfile
            );
            
            return Map(personalProfile.Result, interests.Result, skills.Result, userId);
        }

        private Task<UserIdEntity> GetUserId(string name) =>
            _dependencies.GetUserId(name);

        private Task<List<InterestEntity>> GetInterests(int userId) =>
            _dependencies.GetInterests(userId);
        private Task<List<SkillEntity>> GetSkills(int userId) =>
            _dependencies.GetSkills(userId);

        private Task<PersonalProfileEntity> GetPersonalProfile(int userId) =>
            _dependencies.GetPersonalProfile(userId);

        private PersonalProfileDto Map(PersonalProfileEntity personalProfile, List<InterestEntity> interests,
            List<SkillEntity> skills, UserIdEntity userId)
        {
            return new PersonalProfileDto()
            {
                Description = personalProfile.Description,
                Email = personalProfile.Email,
                FirstName = personalProfile.FirstName,
                LastName = personalProfile.LastName,
                GitHub = personalProfile.GitHub,
                UserId = userId.UserId,
                Phone = personalProfile.Phone,
                Website = personalProfile.Website,
                Interests = interests.Select(a => a.Description).ToList(),
                Skills = skills.Select(a => new SkillDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Punctuation = a.Punctuation
                }).ToList()
            };
        }

    }
}
