using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebPersonal.Shared.DTO;

namespace WebPersonal.BackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkExperienceController : ControllerBase
    {
        [HttpGet("{userId}")]
        public Task<WorkExperienceDto> Get(int userId)
        {
            if (userId != 1)
                throw new NotImplementedException();

            //TODO: Demo - this is to simulate a real scenario
            var workExperienceDto = new WorkExperienceDto()
            {
                Positions = new List<PositionDto>()
                {
                    new PositionDto()
                    {
                         Id  = 1 ,
                        City = "Zaragoza",
                        Country = "Spain",
                        CompanyName = "Simply Supermercados",
                        EndDate = new DateTime(2014,06,30),
                        PositionName = "Front-End Developer",
                        StartDate = new DateTime(2014,01,05),
                        Environment = new List<string>(){"PHP", "JavaScript", "ExtJs", "Valence", "CodeIgniter", },
                        MainProjects = new List<WorkProjectDto>()
                        {
                            new WorkProjectDto()
                            {
                                Id = 2,
                                Nombre = "Reportes Tienda",
                                Description = "CReación de una aplicación utilizando ExtJS con gráficos y colorines que muestra las ventas de una tienda."
                            },
                            new WorkProjectDto()
                            {
                                Id = 1,
                                Nombre  = "Calendario de vacaciones",
                                Description = "Creación de una aplicación en las cuales las tiendas podian asignar vacaciones, tanto de la tienda en si (public holiday) como de los empleados, escirrta en extJs"
                            }
                        }
                    }
                }
            };


            return Task.FromResult(workExperienceDto);
        }

        [HttpPost]
        public Task<WorkExperienceDto> Post(WorkExperienceDto workExperience)
        {
            //Guardar perfil en la base de datos.
            throw new NotImplementedException();
        }
    }
}