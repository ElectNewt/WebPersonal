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
    [Produces("application/json")]
    public class PerfilPersonalController : ControllerBase
    {
        private readonly PersonalProfile _getPersonalProfile;
        private readonly PutPersonalProfile _putPersonalProfile;
        private readonly PostPersonalProfile _postPersonalProfile;

        public PerfilPersonalController(PersonalProfile getPersonalProfile, PutPersonalProfile putPersonalProfile, PostPersonalProfile postPersonalProfile)
        {
            _getPersonalProfile = getPersonalProfile;
            _putPersonalProfile = putPersonalProfile;
            _postPersonalProfile = postPersonalProfile;
        }

        [HttpGet("{userName}")]
        public async Task<ResultDto<PersonalProfileDto>> Get(string userName)
        {
            return (await GetProfile(userName)).MapDto(x=>x);
        }
       
        private async Task<Result<PersonalProfileDto>> GetProfile(string userName) {
            return await _getPersonalProfile.GetPersonalProfileDto(userName);
        }

        [HttpPost]
        public async Task<Result<PersonalProfileDto>> Post(PersonalProfileDto profileDto)
        {
            return await _postPersonalProfile.Create(profileDto)
                .Bind(x => GetProfile(x.UserName));
        }
        
        [HttpPost("returnonlyid")]
        public async Task<Result<int?>> PostId(PersonalProfileDto profileDto)
        {
            return await _postPersonalProfile.Create(profileDto)
                .MapAsync(x=>x.UserId);
        }

        [HttpPut]
        public async Task<Result<PersonalProfileDto>> Put(PersonalProfileDto profileDto)
        {
            return await _putPersonalProfile.Create(profileDto)
                .Bind(x => GetProfile(x.UserName));
        }
    }

}