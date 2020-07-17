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
        private readonly PersonalProfile GetPersonalProfile;

        public PerfilPersonalController(PersonalProfile getPersonalProfile)
        {
            GetPersonalProfile = getPersonalProfile;
        }

        [HttpGet("{userName}")]
        public async Task<Result<PersonalProfileDto>> Get(string userName)
        {
            return await GetPersonalProfile.GetPersonalProfileDto(userName);
        }

        [HttpPost]
        public Task<PersonalProfileDto> Post(PersonalProfileDto profileDto)
        {
            //Guardar perfil en la base de datos.
            throw new NotImplementedException();
        }

    }

}