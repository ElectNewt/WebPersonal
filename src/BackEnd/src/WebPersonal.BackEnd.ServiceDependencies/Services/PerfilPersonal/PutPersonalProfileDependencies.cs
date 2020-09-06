using System.Collections.Generic;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories;
using WebPersonal.BackEnd.Service.PerfilPersonal;
using WebPersonal.Shared.ROP;

namespace WebPersonal.BackEnd.ServiceDependencies.Services.PerfilPersonal
{
    public class PutPersonalProfileDependencies : IPutPersonalProfileDependencies
    {
        private readonly PersonalProfileRepository _personalProfileRepo;
        private readonly SkillRepository _skillRepo;
        private readonly InterestsRepository _interestsRepository;
        private readonly UserIdRepository _userIdRepository;

        public PutPersonalProfileDependencies(PersonalProfileRepository personalProfileRepo, SkillRepository skillRepo,
            InterestsRepository interestsRepository, UserIdRepository userIdRepository)
        {
            _personalProfileRepo = personalProfileRepo;
            _skillRepo = skillRepo;
            _interestsRepository = interestsRepository;
            _userIdRepository = userIdRepository;
        }

        public Task<UserIdEntity?> GetUser(string userName) =>
            _userIdRepository.GetByUserName(userName);

        public async Task<Result<PersonalProfileEntity>> InsertPersonalProfile(PersonalProfileEntity personalProfile) =>
            await _personalProfileRepo.InsertSingle(personalProfile);

        public async Task<Result<PersonalProfileEntity>> UpdatePersonalProfile(PersonalProfileEntity personalProfile) =>
            await _personalProfileRepo.UpdateSingle(personalProfile);

        public async Task<Result<List<SkillEntity>>> InsertSkills(List<SkillEntity> skills) =>
            await _skillRepo.InsertList(skills);

        public async Task<Result<List<SkillEntity>>> UpdateSkills(List<SkillEntity> skills) =>
            await _skillRepo.UpdateList(skills);
        public async Task DeleteUnusedSkills(List<int> unusedIds, int userId) =>
            await _skillRepo.DeleteUnused(unusedIds, userId);

        public async Task<Result<List<InterestEntity>>> InsertInterests(List<InterestEntity> interests) =>
            await _interestsRepository.InsertList(interests);

        public async Task<Result<List<InterestEntity>>> UpdateInterests(List<InterestEntity> interests) =>
            await _interestsRepository.UpdateList(interests);

        public async Task DeleteUnusedInterests(List<int> unusedIds, int userId) =>
            await _interestsRepository.DeleteUnused(unusedIds, userId);

        public async Task CommitTransaction()
        {
            await _skillRepo.CommitTransaction();
            await _personalProfileRepo.CommitTransaction();
            await _interestsRepository.CommitTransaction();
        }

        
    }
}
