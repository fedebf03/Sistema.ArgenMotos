using System.ComponentModel.DataAnnotations;

namespace Sistema_ArgenMotos.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "El Email del vendedor es un campo obligatorio")]
        [EmailAddress]
        public string EmailVendedor { get; set; }

        [Required(ErrorMessage = "La contraseña es un campo obligatorio.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        public string Password { get; set; }
    }
}
