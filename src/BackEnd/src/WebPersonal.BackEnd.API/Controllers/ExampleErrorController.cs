using System;
using Microsoft.AspNetCore.Mvc;
using ROP;
using ROP.APIExtensions;

namespace WebPersonal.BackEnd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Result.Failure(Guid.Parse("ce6887fb-f8fa-49b7-bcb4-d8538b6c9932"))
                .ToActionResult();
        }
    }
}