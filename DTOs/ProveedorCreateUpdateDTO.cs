using Sistema_ArgenMotos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sistema_ArgenMotos.DTOs
{
    public class ProveedorCreateUpdateDTO
    {
        [Required(ErrorMessage = "La razón social es obligatoria.")]
        public string RazonSocial { get; set; }

        [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El CUIL es obligatorio.")]
        [MaxLength(13, ErrorMessage = "El CUIL no puede tener más de 13 caracteres.")]
        [RegularExpression(@"^\d{2}-\d{8}-\d{1}$", ErrorMessage = "El CUIL debe tener el formato XX-XXXXXXXX-X.")]
        public string CUIL { get; set; }

        [MaxLength(200, ErrorMessage = "El domicilio no puede tener más de 200 caracteres.")]
        public string Domicilio { get; set; }

        [Phone(ErrorMessage = "El número de teléfono no es válido.")]
        public string Telefono { get; set; }

        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        public string Email { get; set; }

        public EstadoProveedor Estado { get; set; } = EstadoProveedor.Activo;
    }
}
