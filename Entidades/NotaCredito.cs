using System.ComponentModel.DataAnnotations;

namespace Sistema_ArgenMotos.Entidades
{
    public class NotaCredito
    {
        [Key]
        public int NotaCreditoId { get; set; }

        [Required]
        public int VentaId { get; set; }
        public Venta Venta { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El importe de la nota de crédito debe ser un valor positivo.")]
        public decimal Importe { get; set; }

        [Required]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [Required]
        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }

        public ICollection<NotaCredito_Articulo> Articulos { get; set; }
    }
}
