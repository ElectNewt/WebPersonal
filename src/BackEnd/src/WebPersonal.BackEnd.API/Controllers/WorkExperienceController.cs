using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebPersonal.Shared.DTO;

namespace WebPersonal.BackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkExperienceController : ControllerBase
    {
        [HttpGet("{id}")]
        public Task<WorkExperienceDto> Get(int id)
        {
            //Codigo para leer de la base de datos.
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<WorkExperienceDto> Post(WorkExperienceDto workExperience)
        {
            //Guardar perfil en la base de datos.
            throw new NotImplementedException();
        }
    }
}