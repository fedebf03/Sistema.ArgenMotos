using Sistema_ArgenMotos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sistema_ArgenMotos.Entidades
{
    public class Vendedor
    {
        [Key]
        public int VendedorId { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression(@"^\d+$")]
        public string DNI { get; set; }

        [Required]
        [StringLength(50)]
        public string Legajo { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Phone]
        [StringLength(15)]
        public string Telefono { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public EstadoVendedor Estado { get; set; }
    }
}
