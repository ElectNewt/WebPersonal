using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Translations;
using WebPersonal.Shared.Dto;
using WebPersonal.Shared.Language.Extensions;
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
        private readonly IDataProtector _protector;
        private readonly TraduccionErrores _traduccionErrores;

        public PersonalProfile(IGetPersonalProfileDependencies dependencies, IDataProtectionProvider provider, 
            IHttpContextAccessor httpcontextAccessor)
        {
            _dependencies = dependencies;
            _protector = provider.CreateProtector("PersonalProfile.Protector");
            _traduccionErrores = new TraduccionErrores(httpcontextAccessor.HttpContext.Request.Headers.GetCultureInfo());
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
            return userIdentty == null || userIdentty.UserId == null?
                Result.Failure<UserIdEntity>(Error.Create(_traduccionErrores.IdentityNotFound))
                : userIdentty;
        }


        private async Task<Result<List<InterestEntity>>> GetInterests(UserIdEntity user) =>
            await _dependencies.GetInterests(Convert.ToInt32(user.UserId));

        private async Task<Result<List<SkillEntity>>> GetSkills(UserIdEntity user) =>
            await _dependencies.GetSkills(Convert.ToInt32(user.UserId));

        private async Task<Result<PersonalProfileEntity>> GetPersonalProfileInfo(UserIdEntity user)
        {
            var personalProfile = await _dependencies.GetPersonalProfile(Convert.ToInt32(user.UserId));

            return personalProfile == null ?
                 Result.Failure<PersonalProfileEntity>(Error.Create(_traduccionErrores.PersonalProfile))
                 : personalProfile;
        }

        private Task<PersonalProfileDto> Map((PersonalProfileEntity personalProfile, List<SkillEntity> skills,
            List<InterestEntity> interests) values, UserIdEntity userId)
        {

            PersonalProfileDto profile = new PersonalProfileDto()
            {
                Description = values.personalProfile.Description,
                Email = _protector.Unprotect(values.personalProfile.Email),
                FirstName = values.personalProfile.FirstName,
                LastName = values.personalProfile.LastName,
                GitHub = values.personalProfile.GitHub,
                UserId = userId.UserId,
                UserName = userId.UserName,
                Phone = _protector.Unprotect(values.personalProfile.Phone),
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
