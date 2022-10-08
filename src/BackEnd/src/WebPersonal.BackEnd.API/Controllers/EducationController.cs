using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebPersonal.Shared.Dto;

namespace WebPersonal.BackEnd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        [HttpGet("{userId}")]
        public Task<List<EducationDto>> Get(int userId)
        {
            if (userId != 1)
                throw new NotImplementedException();

            //TODO: Demo - this is to simulate a real scenario
            var educationList = new List<EducationDto>()
            {
                new EducationDto()
                {
                    Id = 2,
                    CourseName ="Master Curso 2",
                    EndDate = new DateTime(2019,07,30),
                    StartDate = new DateTime(2019, 01,23),
                    UniversityName = "University 1"
                },
                new EducationDto()
                {
                    Id = 1,
                    CourseName ="Curso 1",
                    EndDate = new DateTime(2019,01,02),
                    StartDate = new DateTime(2015, 09,14 ),
                    UniversityName = "University 1"
                }
            };
            return Task.FromResult(educationList);
        }

        [HttpPost]
        public Task<EducationDto> Post(EducationDto education)
        {
            throw new NotImplementedException();
        }
    }
}