using Sistema_ArgenMotos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sistema_ArgenMotos.Entidades
{
    public class Proveedor
    {
        [Key]
        public int ProveedorId { get; set; }

        [Required]
        public string RazonSocial { get; set; }

        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required, MaxLength(13)]
        [RegularExpression(@"^\d{2}-\d{8}-\d{1}$")]
        public string CUIL { get; set; }

        [MaxLength(200)]
        public string Domicilio { get; set; }

        [Phone]
        public string Telefono { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public EstadoProveedor Estado { get; set; } = EstadoProveedor.Activo;
    }
}
