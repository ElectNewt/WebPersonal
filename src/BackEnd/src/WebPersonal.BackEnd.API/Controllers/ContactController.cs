using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPersonal.Shared.Dto;
using WebPersonal.Shared.ROP;

namespace WebPersonal.BackEnd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        [HttpPost]
        public Task<ResultDto<ContactResponse>> Post(ContactDto Contacto)
        {
            return Task.FromResult(new ContactResponse()
            {
                MessageSent = true
            }.Success()
            .MapDto(x => x));
        }
    }
}
