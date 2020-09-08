using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Service.Mappers;
using WebPersonal.Shared.Dto;
using WebPersonal.Shared.ROP;

namespace WebPersonal.BackEnd.Service.PerfilPersonal
{
    public interface IPostPersonalProfileDependencies
    {
        Task<UserIdEntity> InsertUserId(string name);
        Task<Result<PersonalProfileEntity>> InsertPersonalProfile(PersonalProfileEntity personalProfile);
        Task<Result<List<SkillEntity>>> InsertSkills(List<SkillEntity> skills);
        Task<Result<List<InterestEntity>>> InsertInterests(List<InterestEntity> interests);
        Task CommitTransaction();
    }

    public class PostPersonalProfile
    {
        private readonly IPostPersonalProfileDependencies _dependencies;

        public PostPersonalProfile(IPostPersonalProfileDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public async Task<Result<UserIdEntity>> Create(PersonalProfileDto personalProfile)
        {

            return await CreateNewUser(personalProfile)
                .Bind(x => SavePersonalProfile(x, personalProfile))
                .Bind(x => SaveSkills(x, personalProfile))
                .Bind(x => SaveInterests(x, personalProfile))
                .Bind(CommitTransaction);
        }

        private async Task<Result<UserIdEntity>> CreateNewUser(PersonalProfileDto personalProfile)
        {
            return await _dependencies.InsertUserId(personalProfile.UserName);
        }

        private async Task<Result<UserIdEntity>> SavePersonalProfile(UserIdEntity user, PersonalProfileDto personalProfile)
        {
            return await _dependencies.InsertPersonalProfile(personalProfile.Map(Convert.ToInt32(user.UserId)))
                .MapAsync(_ => user);
        }

        private async Task<Result<UserIdEntity>> SaveSkills(UserIdEntity user, PersonalProfileDto personalProfile)
        {
            return await _dependencies.InsertSkills(personalProfile.Skills.Map(Convert.ToInt32(user.UserId)))
                .MapAsync(_ => user);
        }

        private async Task<Result<UserIdEntity>> SaveInterests(UserIdEntity user, PersonalProfileDto personalProfile)
        {
            return await _dependencies.InsertInterests(personalProfile.Interests.Map(Convert.ToInt32(user.UserId)))
                .MapAsync(_ => user);
        }
        private async Task<Result<UserIdEntity>> CommitTransaction(UserIdEntity user)
        {
            await _dependencies.CommitTransaction();

            return user;
        }
    }
}
