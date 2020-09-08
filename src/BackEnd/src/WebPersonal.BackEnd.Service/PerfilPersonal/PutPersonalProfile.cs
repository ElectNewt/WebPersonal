using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Service.Mappers;
using WebPersonal.BackEnd.Service.Validations;
using WebPersonal.Shared.Dto;
using WebPersonal.Shared.ROP;

namespace WebPersonal.BackEnd.Service.PerfilPersonal
{

    public interface IPutPersonalProfileDependencies
    {
        Task<UserIdEntity?> GetUser(string userName);
        Task CommitTransaction();
        Task<Result<PersonalProfileEntity>> InsertPersonalProfile(PersonalProfileEntity personalProfile);
        Task<Result<PersonalProfileEntity>> UpdatePersonalProfile(PersonalProfileEntity personalProfile);
        Task<Result<List<SkillEntity>>> InsertSkills(List<SkillEntity> skills);
        Task<Result<List<SkillEntity>>> UpdateSkills(List<SkillEntity> skills);
        Task DeleteUnusedSkills(List<int> unusedIds, int userId);
        Task<Result<List<InterestEntity>>> InsertInterests(List<InterestEntity> interests);
        Task<Result<List<InterestEntity>>> UpdateInterests(List<InterestEntity> interests);
        Task DeleteUnusedInterests(List<int> unusedIds, int userId);
    }

    public class PutPersonalProfile
    {
        private readonly IPutPersonalProfileDependencies _dependencies;

        public PutPersonalProfile(IPutPersonalProfileDependencies dependencies)
        {
            _dependencies = dependencies;
        }


        public async Task<Result<PersonalProfileDto>> Create(PersonalProfileDto personalProfile)
        {
            return await ValidateUsuerId(personalProfile)
                 .Bind(ValidateProfile)
                 .Bind(ValidateUserName)
                 .Bind(MapToEntities)
                 .Bind(SavePersonalProfile)
                 .Bind(SaveSkills)
                 .Bind(SaveInterests)
                 .Ignore()
                 .Bind(CommitTransaction)
                 .MapAsync(_ => personalProfile);
        }

        private Task<Result<PersonalProfileDto>> ValidateUsuerId(PersonalProfileDto personalProfile)
        {
            if (personalProfile.UserId == null)
            {
                return Result.Failure<PersonalProfileDto>("UsuarioId no encontrado").Async();
            }
            return personalProfile.Success().Async();
        }

        private Task<Result<PersonalProfileDto>> ValidateProfile(PersonalProfileDto personalProfile)
            => personalProfile.ValidateDto().Async();


        //TODO: not sure if this bit should be here
        private async Task<Result<PersonalProfileDto>> ValidateUserName(PersonalProfileDto personalProfile)
        {
            UserIdEntity? user = await _dependencies.GetUser(personalProfile.UserName);

            if (user != null && user.UserId != personalProfile.UserId)
            {
                return Result.Failure<PersonalProfileDto>($"El nombre de usuario {personalProfile.UserName} ya está en uso");
            }

            return personalProfile;

        }

        private Task<Result<PostPersonalProfileWrapper>> MapToEntities(PersonalProfileDto personalProfile)
        {
            return personalProfile.MapToWraperEntities().Success().Async();
        }

        private async Task<Result<PostPersonalProfileWrapper>> SavePersonalProfile(PostPersonalProfileWrapper postPPWraper)
        {
            if (postPPWraper.personalProfile.Id == null)
                await _dependencies.InsertPersonalProfile(postPPWraper.personalProfile);

            await _dependencies.UpdatePersonalProfile(postPPWraper.personalProfile);

            return postPPWraper;
        }

        private async Task<Result<PostPersonalProfileWrapper>> SaveSkills(PostPersonalProfileWrapper postPPWraper)
        {
            return await postPPWraper.skillEntities
                .Success()
                .Async()
                .Bind(x => DeleteUnusedSkills(x, postPPWraper.personalProfile.UserId))
                .Bind(UpdateSkills)
                .Bind(InsertSkills)
                .MapAsync(_ => postPPWraper);

            async Task<Result<List<SkillEntity>>> DeleteUnusedSkills(List<SkillEntity> skills, int userId)
            {
                await _dependencies.DeleteUnusedSkills(
                    skills.Where(a => a.Id != null).Select(a => Convert.ToInt32(a.Id)).Distinct().ToList(), userId);

                return skills;
            }

            async Task<Result<List<SkillEntity>>> UpdateSkills(List<SkillEntity> skills)
            {
                return (await _dependencies.UpdateSkills(skills.Where(a => a.Id != null).ToList()))
                    .Map(_ => skills);
            }

            async Task<Result<List<SkillEntity>>> InsertSkills(List<SkillEntity> skills)
            {
                return (await _dependencies.InsertSkills(skills.Where(a => a.Id == null).ToList()))
                    .Map(_ => skills);
            }
        }

        private async Task<Result<PostPersonalProfileWrapper>> SaveInterests(PostPersonalProfileWrapper postPPWraper)
        {
            return await postPPWraper.interestEntities
                .Success()
                .Async()
                .Bind(x => DeleteUnusedInterests(x, postPPWraper.personalProfile.UserId))
                .Bind(UpdateInterests)
                .Bind(InsertInterests)
                .MapAsync(_ => postPPWraper);

            async Task<Result<List<InterestEntity>>> DeleteUnusedInterests(List<InterestEntity> interests, int userId)
            {
                await _dependencies.DeleteUnusedInterests(
                    interests.Where(a => a.Id != null).Select(a => Convert.ToInt32(a.Id)).Distinct().ToList(), userId);

                    return interests;
            }

            async Task<Result<List<InterestEntity>>> UpdateInterests(List<InterestEntity> interests)
            {
                return (await _dependencies.UpdateInterests(interests.Where(a => a.Id != null).ToList()))
                    .Map(_ => interests);
            }

            async Task<Result<List<InterestEntity>>> InsertInterests(List<InterestEntity> interests)
            {
                return (await _dependencies.InsertInterests(interests.Where(a => a.Id == null).ToList()))
                    .Map(_ => interests);
            }
        }

        private async Task<Result<Unit>> CommitTransaction(Unit _)
        {
            await _dependencies.CommitTransaction();

            return Result.Unit;
        }
    }
}
