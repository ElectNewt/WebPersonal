using System.ComponentModel.DataAnnotations;

namespace WebPersonal.Shared.Dto
{
    public class ContactDto
    {
        [Required(ErrorMessage = "Por favor indica tu nombre")]
        public string Name { get; set; }
        public string Surname { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Debes indicar un email válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El contenido del mensaje es obligatorio")]
        public string Message { get; set; }
    }
}
