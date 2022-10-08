using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebPersonal.Shared.Dto;

namespace WebPersonal.BackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicProjectsController : ControllerBase
    {
        [HttpGet("{userId}")]
        public Task<AcademicProjectsDto> Get(int userId)
        {
            if (userId != 1)
                throw new NotImplementedException();

            //TODO: Demo - this is to simulate a real scenario
            var academicProjects = new AcademicProjectsDto()
            {
                Projects = new List<AcademicProjectDto>()
                {
                    new AcademicProjectDto()
                    {
                        Id=1,
                        Details = "Aplicación para suibr imagenes a internet, con la posiblidad de retocarlas con filtros y redimensionar",
                        Environment = new List<string>(){"PHP","JavaScript", "Bootstrap"},
                        Name = "IMGLovely"
                    }
                }
            };

            return Task.FromResult(academicProjects);
        }

        [HttpPost]
        public Task<AcademicProjectsDto> Post(AcademicProjectsDto projects)
        {
            throw new NotImplementedException();
        }
    }
}