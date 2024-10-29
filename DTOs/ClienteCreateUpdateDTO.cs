using Sistema_ArgenMotos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sistema_ArgenMotos.DTOs
{
    public class ClienteCreateUpdateDTO
    {
        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [MaxLength(10, ErrorMessage = "El DNI no puede tener más de 10 caracteres.")]
        public string DNI { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El apellido no puede tener más de 100 caracteres.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El tipo de cliente es obligatorio.")]
        public TipoCliente Tipo { get; set; }

        [Phone(ErrorMessage = "El número de teléfono no es válido.")]
        public string Telefono { get; set; }

        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Email { get; set; }

        [MaxLength(200, ErrorMessage = "El domicilio no puede tener más de 200 caracteres.")]
        public string Domicilio { get; set; }

        public EstadoCliente Estado { get; set; } = EstadoCliente.Activo;
    }
}
