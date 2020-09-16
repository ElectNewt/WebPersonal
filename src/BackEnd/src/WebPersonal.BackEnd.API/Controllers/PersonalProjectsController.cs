using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebPersonal.Shared.Dto;

namespace WebPersonal.BackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalProjectsController : ControllerBase
    {
        [HttpGet("{userId}")]
        public Task<PersonalProjectsDto> Get(int userId)
        {
            if (userId != 1)
                throw new NotImplementedException();

            //TODO: Demo - this is to simulate a real scenario
            var personalPRojectsDto = new PersonalProjectsDto()
            {
                PersonalProjects = new List<PersonalProjectDto>()
                {
                    new PersonalProjectDto()
                    {
                        Id = 2,
                        Description = "WEb para compartir conocimiento sobre programación",
                        Name = "NetMentor",
                        ProjectType = "Website",
                        Environment = new List<string>(){"c#", ".NET", "NetCore","Linux", "Mysql"}

                    },
                    new PersonalProjectDto()
                    {
                        Id = 1,
                        Description = "Aplicación para parsear Ficheros CSV en objetos C#",
                        Name = "CSV Parser",
                        ProjectType = "Library",
                        Environment = new List<string>(){"c#", ".NET", "Net Standard", "csv"}

                    }
                }
            };


            return Task.FromResult(personalPRojectsDto);

        }

        public Task<PersonalProjectsDto> Post(PersonalProjectsDto projects)
        {
            throw new NotImplementedException();
        }
    }
}