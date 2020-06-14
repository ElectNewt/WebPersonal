using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPersonal.Shared.Dto;

namespace WebPersonal.BackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalProjectsController : ControllerBase
    {
        [HttpGet("{id}")]
        public Task<PersonalProjectsDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PersonalProjectsDto> Post(PersonalProjectsDto projects)
        {
            throw new NotImplementedException();
        }
    }
}