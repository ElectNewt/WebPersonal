﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ROP;
using WebPersonal.Backend.EmailService;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories;
using WebPersonal.BackEnd.Service.PerfilPersonal;

namespace WebPersonal.BackEnd.ServiceDependencies.Services.PerfilPersonal
{
    public class PostPersonalProfileDependencies : IPostPersonalProfileDependencies
    {
        private readonly PersonalProfileRepository _personalProfileRepo;
        private readonly SkillRepository _skillRepo;
        private readonly InterestsRepository _interestsRepository;
        private readonly UserIdRepository _userIdRepository;
        private readonly IEmailSender _emailSender;

        public PostPersonalProfileDependencies(PersonalProfileRepository personalProfileRepo, SkillRepository skillRepo,
           InterestsRepository interestsRepository, UserIdRepository userIdRepository, IEmailSender emailSender)
        {
            _personalProfileRepo = personalProfileRepo;
            _skillRepo = skillRepo;
            _interestsRepository = interestsRepository;
            _userIdRepository = userIdRepository;
            _emailSender = emailSender;
        }


        public async Task CommitTransaction()
        {
            await _personalProfileRepo.CommitTransaction();
            await _skillRepo.CommitTransaction();
            await _interestsRepository.CommitTransaction();
            await _userIdRepository.CommitTransaction();
        }

        public async Task<Result<PersonalProfileEntity>> InsertPersonalProfile(PersonalProfileEntity personalProfile) =>
            await _personalProfileRepo.InsertSingle(personalProfile);
        
        public async Task<Result<List<SkillEntity>>> InsertSkills(List<SkillEntity> skills) =>
            await _skillRepo.InsertList(skills);

        public async Task<Result<List<InterestEntity>>> InsertInterests(List<InterestEntity> interests) =>
            await _interestsRepository.InsertList(interests);

        public async Task DeleteUnusedInterests(List<int> unusedIds, int userId) =>
            await _interestsRepository.DeleteUnused(unusedIds, userId);

        public async Task<UserIdEntity> InsertUserId(string name) =>
            await _userIdRepository.InsertSingle(UserIdEntity.Create(name, null));

        public async Task<Result<bool>> SendEmail(string to, string subject, string body) =>
            await _emailSender.SendEmail(to, subject, body);
    }
}
