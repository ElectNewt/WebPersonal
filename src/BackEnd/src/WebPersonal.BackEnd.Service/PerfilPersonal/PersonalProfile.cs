using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.Shared.Dto;
using WebPersonal.Shared.ROP;

namespace WebPersonal.BackEnd.Service.PerfilPersonal
{
    public interface IGetPersonalProfileDependencies
    {
        Task<UserIdEntity?> GetUserId(string name);
        Task<List<InterestEntity>> GetInterests(int userId);
        Task<List<SkillEntity>> GetSkills(int iduserId);
        Task<PersonalProfileEntity?> GetPersonalProfile(int userId);
    }

    public class PersonalProfile
    {
        private readonly IGetPersonalProfileDependencies _dependencies;

        public PersonalProfile(IGetPersonalProfileDependencies dependencies)
        {
            _dependencies = dependencies;
        }


        public async Task<Result<PersonalProfileDto>> GetPersonalProfileDto(string name)
        {

            Result<UserIdEntity> userId = await GetUserId(name);

            return await userId.Async()
                .ThenCombine(GetPersonalProfileInfo)
                .ThenCombine(GetSkills)
                .ThenCombine(GetInterests)
                .GetCombined()
                .MapAsync(x => Map(x, userId.Value));
        }


        private async Task<Result<UserIdEntity>> GetUserId(string name)
        {
            var userIdentty = await _dependencies.GetUserId(name);
            return userIdentty == null ?
                Result.Failure<UserIdEntity>(Error.Create("UserIdentity no encontrado"))
                : userIdentty;
        }


        private async Task<Result<List<InterestEntity>>> GetInterests(UserIdEntity user) =>
            await _dependencies.GetInterests(user.UserId);

        private async Task<Result<List<SkillEntity>>> GetSkills(UserIdEntity user) =>
            await _dependencies.GetSkills(user.UserId);

        private async Task<Result<PersonalProfileEntity>> GetPersonalProfileInfo(UserIdEntity user)
        {
            var personalProfile = await _dependencies.GetPersonalProfile(user.UserId);

            return personalProfile == null ?
                 Result.Failure<PersonalProfileEntity>(Error.Create("personal profile no encontrado"))
                 : personalProfile;
        }

        private Task<PersonalProfileDto> Map((PersonalProfileEntity personalProfile, List<SkillEntity> skills,
            List<InterestEntity> interests) values, UserIdEntity userId)
        {

            PersonalProfileDto profile = new PersonalProfileDto()
            {
                Description = values.personalProfile.Description,
                Email = values.personalProfile.Email,
                FirstName = values.personalProfile.FirstName,
                LastName = values.personalProfile.LastName,
                GitHub = values.personalProfile.GitHub,
                UserId = userId.UserId,
                UserName = userId.UserName,
                Phone = values.personalProfile.Phone,
                Website = values.personalProfile.Website,
                Id = values.personalProfile.Id,
                Interests = values.interests.Select(a => new InterestDto()
                {
                    Id = a.Id,
                    Interest = a.Description
                }).ToList(),
                Skills = values.skills.Select(a => new SkillDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Punctuation = a.Punctuation
                }).ToList()
            };
            return Task.FromResult(profile);
        }

    }
}
