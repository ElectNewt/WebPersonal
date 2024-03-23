using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ROP;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Service.Mappers;
using WebPersonal.Shared.Dto;

namespace WebPersonal.BackEnd.Service.PerfilPersonal
{
    public interface IPostPersonalProfileDependencies
    {
        Task<UserIdEntity> InsertUserId(string name);
        Task<Result<PersonalProfileEntity>> InsertPersonalProfile(PersonalProfileEntity personalProfile);
        Task<Result<List<SkillEntity>>> InsertSkills(List<SkillEntity> skills);
        Task<Result<List<InterestEntity>>> InsertInterests(List<InterestEntity> interests);
        Task<Result<bool>> SendEmail(string to, string subject, string body);
        Task CommitTransaction();
    }

    public class PostPersonalProfile
    {
        private readonly IPostPersonalProfileDependencies _dependencies;
        private readonly IDataProtector _protector;

        public PostPersonalProfile(IPostPersonalProfileDependencies dependencies, IDataProtectionProvider provider)
        {
            _dependencies = dependencies;
            _protector = provider.CreateProtector("PersonalProfile.Protector");
        }

        public async Task<Result<UserIdEntity>> Create(PersonalProfileDto personalProfile)
        {

            return await CreateNewUser(personalProfile)
                .Bind(x => SavePersonalProfile(x, personalProfile))
                .Bind(x => SaveSkills(x, personalProfile))
                .Bind(x => SaveInterests(x, personalProfile))
                .Bind(CommitTransaction)
                .Then(_=>SendConfirmationEmail(personalProfile));
        }

        private async Task<Result<UserIdEntity>> CreateNewUser(PersonalProfileDto personalProfile)
        {
            return await _dependencies.InsertUserId(personalProfile.UserName);
        }

        private async Task<Result<UserIdEntity>> SavePersonalProfile(UserIdEntity user, PersonalProfileDto personalProfile)
        {
            return await _dependencies.InsertPersonalProfile(personalProfile.Map(Convert.ToInt32(user.UserId), _protector))
                .Map(_ => user);
        }

        private async Task<Result<UserIdEntity>> SaveSkills(UserIdEntity user, PersonalProfileDto personalProfile)
        {
            return await _dependencies.InsertSkills(personalProfile.Skills.Map(Convert.ToInt32(user.UserId)))
                .Map(_ => user);
        }

        private async Task<Result<UserIdEntity>> SaveInterests(UserIdEntity user, PersonalProfileDto personalProfile)
        {
            return await _dependencies.InsertInterests(personalProfile.Interests.Map(Convert.ToInt32(user.UserId)))
                .Map(_ => user);
        }
        private async Task<Result<UserIdEntity>> CommitTransaction(UserIdEntity user)
        {
            await _dependencies.CommitTransaction();

            return user;
        }
        
        private Task<Result<bool>>SendConfirmationEmail(PersonalProfileDto personalProfile)
        =>  _dependencies.SendEmail($"{personalProfile.UserName}@mail.com", "personal profile created",
                "congratulations");
        
    }
}
