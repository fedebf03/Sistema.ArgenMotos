using Sistema_ArgenMotos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sistema_ArgenMotos.DTOs
{
    public class VendedorCreateUpdateDTO
    {
        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [StringLength(10, ErrorMessage = "El DNI no debe exceder de 10 caracteres.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El DNI solo puede contener números.")]
        public string DNI { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El Nombre no debe exceder de 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Legajo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El Legajo no debe exceder de 50 caracteres.")]
        public string Legajo { get; set; }

        [Required(ErrorMessage = "El Apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El Apellido no debe exceder de 50 caracteres.")]
        public string Apellido { get; set; }

        [Phone(ErrorMessage = "El número de teléfono no es válido.")]
        [StringLength(15, ErrorMessage = "El número de teléfono no debe exceder de 15 caracteres.")]
        public string Telefono { get; set; }

        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        [StringLength(100, ErrorMessage = "El Email no debe exceder de 100 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Estado es obligatorio.")]
        public EstadoVendedor Estado { get; set; }
    }
}
