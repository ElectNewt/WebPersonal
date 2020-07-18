using System.Collections.Generic;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Service.Mappers;
using WebPersonal.BackEnd.Service.Validations;
using WebPersonal.Shared.Dto;
using WebPersonal.Shared.ROP;
#nullable enable

namespace WebPersonal.BackEnd.Service.PerfilPersonal
{

    public interface IPostPersonalProfileDependencies
    {
        Task<UserIdEntity?> GetUser(string userName);
        Task CommitTransaction();
        Task<Result<PersonalProfileEntity>> SavePersonalProfile(PersonalProfileEntity personalProfile);
        Task<Result<List<SkillEntity>>> SaveSkills(List<SkillEntity> skills);
        Task<Result<List<InterestEntity>>> SaveInterests(List<InterestEntity> interests);

    }

    public class PostPersonalProfile
    {
        private readonly IPostPersonalProfileDependencies _dependencies;

        public PostPersonalProfile(IPostPersonalProfileDependencies dependencies)
        {
            _dependencies = dependencies;
        }


        public async Task<Result<PersonalProfileDto>> Create(PersonalProfileDto personalProfile)
        {
            return await ValidateUsuerId(personalProfile)
                 .Bind(ValidateProfile)
                 .Bind(ValidateUserName)
                 .Bind(MapToEntities)
                 .Then(x => _dependencies.SavePersonalProfile(x.Item1))
                 .Then(x => _dependencies.SaveSkills(x.Item2))
                 .Then(x => _dependencies.SaveInterests(x.Item3))
                 .Ignore()
                 .Bind(CommitTransaction)
                 .MapAsync(_=> Task.FromResult(personalProfile));
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

        private Task<Result<(PersonalProfileEntity, List<SkillEntity>, List<InterestEntity>)>> MapToEntities(PersonalProfileDto personalProfile)
            => personalProfile.MapToEntities().Success().Async();

        private async Task<Result<Unit>> CommitTransaction(Unit _)
        {
            await _dependencies.CommitTransaction();

            return Result.Unit;
        }

    }
}
