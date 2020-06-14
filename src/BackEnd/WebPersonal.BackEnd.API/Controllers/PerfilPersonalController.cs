using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebPersonal.Shared.Dto;

namespace WebPersonal.BackEnd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerfilPersonalController : ControllerBase
    {
        [HttpGet("{id}")]
        public Task<PersonalProfileDto> Get(int id)
        {
            //Codigo para leer de la base de datos.
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<PersonalProfileDto> Post(PersonalProfileDto profileDto)
        {
            //Guardar perfil en la base de datos.
            throw new NotImplementedException();
        }

    }

}