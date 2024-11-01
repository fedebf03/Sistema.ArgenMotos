using System.ComponentModel.DataAnnotations;

namespace Sistema_ArgenMotos.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El Email es un campo obligatorio.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es un campo obligatorio.")]
        public string Password { get; set; }
    }
}
