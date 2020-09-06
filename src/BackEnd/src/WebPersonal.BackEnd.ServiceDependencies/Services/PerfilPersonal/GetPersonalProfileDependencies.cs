using System.Collections.Generic;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories;
using WebPersonal.BackEnd.Service.PerfilPersonal;

namespace WebPersonal.BackEnd.ServiceDependencies.Services.PerfilPersonal
{
    public class GetPersonalProfileDependencies : IGetPersonalProfileDependencies
    {
        private readonly PersonalProfileRepository _personalProfileRepo;
        private readonly SkillRepository _skillRepo;
        private readonly InterestsRepository _interestsRepository;
        private readonly UserIdRepository _userIdRepository;

        public GetPersonalProfileDependencies(PersonalProfileRepository personalProfileRepo, SkillRepository skillRepo,
            InterestsRepository interestsRepository, UserIdRepository userIdRepository)
        {
            _personalProfileRepo = personalProfileRepo;
            _skillRepo = skillRepo;
            _interestsRepository = interestsRepository;
            _userIdRepository = userIdRepository;
        }

        public Task<List<InterestEntity>> GetInterests(int userId) =>
            _interestsRepository.GetListByUserId(userId);

        public Task<PersonalProfileEntity?> GetPersonalProfile(int userId)
            => _personalProfileRepo.GetByUserId(userId);

        public Task<List<SkillEntity>> GetSkills(int userId)
            => _skillRepo.GetListByUserId(userId);

        public Task<UserIdEntity?> GetUserId(string name)
            => _userIdRepository.GetByUserName(name);
    }
}
