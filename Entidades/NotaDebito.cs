using System.ComponentModel.DataAnnotations;

namespace Sistema_ArgenMotos.Entidades
{
    public class NotaDebito
    {
        [Key]
        public int NotaDebitoId { get; set; }

        [Required]
        public int VentaId { get; set; }
        public Venta Venta { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El importe de la nota de débito debe ser un valor positivo")]
        public decimal Importe { get; set; }

        [Required]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [Required]
        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }

        public ICollection<NotaDebito_Articulo> Articulos { get; set; }
    }
}