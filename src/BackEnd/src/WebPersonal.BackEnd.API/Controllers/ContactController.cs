using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ROP;
using ROP.APIExtensions;
using WebPersonal.Shared.Dto;

namespace WebPersonal.BackEnd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        [HttpPost]
        public Task<IActionResult> Post(ContactDto Contacto)
        {
            return Task.FromResult(new ContactResponse()
            {
                MessageSent = true
            }.Success()
            .ToActionResult());
        }
    }
}
