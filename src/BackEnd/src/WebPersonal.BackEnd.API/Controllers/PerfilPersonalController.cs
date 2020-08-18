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
        //Commented for the video about dapper
       // private readonly PostPersonalProfile _postPersonalProfile;

        public PerfilPersonalController(PersonalProfile getPersonalProfile)//, PostPersonalProfile postPersonalProfile
        {
            _getPersonalProfile = getPersonalProfile;
           // _postPersonalProfile = postPersonalProfile;
        }

        [HttpGet("{userName}")]
        public async Task<Result<PersonalProfileDto>> Get(string userName)
        {
            return await _getPersonalProfile.GetPersonalProfileDto(userName);
        }

        //[HttpPost]
        //public async Task<Result<PersonalProfileDto>> Post (PersonalProfileDto profileDto)
        //{
        //   return await _postPersonalProfile.Create(profileDto);
        //}

    }

}