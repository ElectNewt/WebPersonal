using System;
using System.ComponentModel.DataAnnotations;

namespace WebPersonal.Shared.Dto
{
    public class GuestbookEntryDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Your name must be between 2 and 60 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a message")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Your message must be between 5 and 500 characters")]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
