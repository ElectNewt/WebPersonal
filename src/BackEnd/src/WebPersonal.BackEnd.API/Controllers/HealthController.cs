using Microsoft.AspNetCore.Mvc;
using System;

namespace WebPersonal.BackEnd.API.Controllers
{
  
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet("ejemplo-docker")]
        public string Get()
        {
            Console.WriteLine("docker");
           return "true";
        }
    }
}
