using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Service.PerfilPersonal;
using WebPersonal.Shared.Dto;
using WebPersonal.Shared.ROP;

namespace WebPersonal.BackEnd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerfilPersonalController : ControllerBase
    {
        private readonly PersonalProfile _getPersonalProfile;
        
        private readonly PutPersonalProfile _putPersonalProfile;

        public PerfilPersonalController(PersonalProfile getPersonalProfile, PutPersonalProfile putPersonalProfile)
        {
            _getPersonalProfile = getPersonalProfile;
            _putPersonalProfile = putPersonalProfile;
        }

        [HttpGet("{userName}")]
        public async Task<Result<PersonalProfileDto>> Get(string userName)
        {
            return await _getPersonalProfile.GetPersonalProfileDto(userName);
        }

        [HttpPost]
        public Task<Result<PersonalProfileDto>> Post(PersonalProfileDto profileDto)
        {
            throw new NotImplementedException();
//            return await _putPersonalProfile.Create(profileDto);
        }

        [HttpPut]
        public async Task<Result<PersonalProfileDto>> Put(PersonalProfileDto profileDto)
        {
            return await _putPersonalProfile.Create(profileDto)
                .Bind(x => Get(x.UserName));
        }

    }

}