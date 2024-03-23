using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ROP;
using ROP.APIExtensions;
using WebPersonal.BackEnd.Service.PerfilPersonal;
using WebPersonal.Shared.Dto;

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
        public async Task<IActionResult> Get(string userName)
        {
            return await GetProfile(userName).ToActionResult();
        }
       
        private async Task<Result<PersonalProfileDto>> GetProfile(string userName) {
            return await _getPersonalProfile.GetPersonalProfileDto(userName);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PersonalProfileDto profileDto)
        {
            return await _postPersonalProfile.Create(profileDto)
                .Bind(x => GetProfile(x.UserName))
                .ToActionResult();
        }
        
        [HttpPost("returnonlyid")]
        public async Task<Result<int?>> PostId(PersonalProfileDto profileDto)
        {
            return await _postPersonalProfile.Create(profileDto)
                .Map(x=>x.UserId);
        }

        [HttpPut]
        public async Task<Result<PersonalProfileDto>> Put(PersonalProfileDto profileDto)
        {
            return await _putPersonalProfile.Create(profileDto)
                .Bind(x => GetProfile(x.UserName));
        }
    }

}