using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPersonal.Shared.Dto;

namespace WebPersonal.BackEnd.API.Controllers.v2
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        [HttpGet("{id}")]
        public Task<EducationDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<EducationDto> Post(EducationDto education)
        {
            throw new NotImplementedException();
        }
    }
}