using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.Shared.Dto;

namespace WebPersonal.BackEnd.Service.Mappers
{
    public static class PersonalProfileDtoMapper
    {
        public static PostPersonalProfileWrapper MapToWraperEntities(this PersonalProfileDto profileDto, IDataProtector protector)
        {
            if(profileDto.UserId == null)
            {
                throw new Exception("Your are trying to build an entity with a null user, that cannot be done");
            }

            string encriptedPhone = protector.Protect(profileDto.Phone);
            string encriptedEmail = protector.Protect(profileDto.Email);

            PersonalProfileEntity personalProfile = PersonalProfileEntity.Create((int)profileDto.UserId, profileDto.Id, profileDto.FirstName, profileDto.LastName, profileDto.Description,
                encriptedPhone, encriptedEmail, profileDto.Website, profileDto.GitHub);

            List<SkillEntity> skills = profileDto.Skills.Select(a => SkillEntity.Create(profileDto.UserId, a.Id, a.Name, a.Punctuation)).ToList();

            List<InterestEntity> interests = profileDto.Interests.Select(a => InterestEntity.Create(a.Id, profileDto.UserId, a.Interest)).ToList();

            return new PostPersonalProfileWrapper(personalProfile, skills, interests);
        }

        public static PersonalProfileEntity Map(this PersonalProfileDto profileDto, int userId, IDataProtector protector)
        {
            string encriptedPhone = protector.Protect(profileDto.Phone);
            string encriptedEmail = protector.Protect(profileDto.Email);

            return PersonalProfileEntity.Create(userId, profileDto.Id, profileDto.FirstName, profileDto.LastName, profileDto.Description,
                encriptedPhone, encriptedEmail, profileDto.Website, profileDto.GitHub);
        }

    }
    public class PostPersonalProfileWrapper
    {
        public readonly PersonalProfileEntity personalProfile;
        public readonly List<SkillEntity> skillEntities;
        public readonly List<InterestEntity> interestEntities;

        public PostPersonalProfileWrapper(PersonalProfileEntity personalProfile, List<SkillEntity> skillEntities, List<InterestEntity> interestEntities)
        {
            this.personalProfile = personalProfile;
            this.skillEntities = skillEntities;
            this.interestEntities = interestEntities;
        }
    }
}
