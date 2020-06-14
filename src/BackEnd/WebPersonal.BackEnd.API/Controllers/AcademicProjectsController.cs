using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPersonal.Shared.Dto;

namespace WebPersonal.BackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicProjectsController : ControllerBase
    {
        [HttpGet("{id}")]
        public Task<AcademicProjectsDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AcademicProjectsDto> Post(AcademicProjectsDto projects)
        {
            throw new NotImplementedException();
        }
    }
}