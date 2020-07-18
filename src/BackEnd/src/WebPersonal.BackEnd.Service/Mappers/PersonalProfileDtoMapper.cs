using System.Collections.Generic;
using System.Linq;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.Shared.Dto;

namespace WebPersonal.BackEnd.Service.Mappers
{
    public static class PersonalProfileDtoMapper
    {
        public static (PersonalProfileEntity, List<SkillEntity>, List<InterestEntity>) MapToEntities(this PersonalProfileDto profileDto)
        {

            PersonalProfileEntity personalProfile = PersonalProfileEntity.Create((int)profileDto.UserId, profileDto.Id, profileDto.FirstName, profileDto.LastName, profileDto.Description,
                profileDto.Phone, profileDto.Email, profileDto.Website, profileDto.GitHub);

            List<SkillEntity> skills = profileDto.Skills.Select(a => SkillEntity.Create(profileDto.UserId, a.Id, a.Name, a.Punctuation)).ToList();

            List<InterestEntity> interests = profileDto.Interests.Select(a => InterestEntity.Create(a.Id, profileDto.UserId, a.Interest)).ToList();

            return (personalProfile, skills, interests);
        }

    }
}
