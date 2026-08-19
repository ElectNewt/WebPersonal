using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebPersonal.Shared.Dto;

namespace WebPersonal.BackEnd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestbookController : ControllerBase
    {
        private static readonly List<GuestbookEntryDto> Entries = new List<GuestbookEntryDto>
        {
            new GuestbookEntryDto
            {
                Id = 1,
                Name = "Marta",
                Email = "marta@example.com",
                Message = "Really enjoyed looking through your project work.",
                CreatedAt = DateTime.Now.AddDays(-3)
            },
            new GuestbookEntryDto
            {
                Id = 2,
                Name = "Daniel",
                Email = "daniel@example.com",
                Message = "The profile is clear and easy to navigate.",
                CreatedAt = DateTime.Now.AddDays(-1)
            }
        };

        [HttpGet]
        public ActionResult<List<GuestbookEntryDto>> Get(string search = "")
        {
            IEnumerable<GuestbookEntryDto> results = Entries;

            if (!string.IsNullOrWhiteSpace(search))
            {
                results = results.Where(entry =>
                    entry.Name.ToLower().Contains(search.ToLower()) ||
                    entry.Message.ToLower().Contains(search.ToLower()));
            }

            return Ok(results.OrderBy(entry => entry.CreatedAt).ToList());
        }

        [HttpPost]
        public ActionResult<GuestbookEntryDto> Post(GuestbookEntryDto entry)
        {
            entry.Id = Entries.Count + 1;
            entry.CreatedAt = DateTime.Now;
            Entries.Add(entry);

            return Ok(entry);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entry = Entries.FirstOrDefault(item => item.Id == id);

            if (entry == null)
            {
                return Ok();
            }

            Entries.Remove(entry);
            return Ok();
        }
    }
}
