using System.ComponentModel.DataAnnotations;

namespace Sistema_ArgenMotos.Entidades
{
    public class Venta
    {
        [Key]
        public int VentaId { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El precio final debe ser un valor positivo.")]
        public decimal PrecioFinal { get; set; }

        [Required]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [Required]
        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }

        public ICollection<Venta_Articulo> Articulos { get; set; }

        public Factura Factura { get; set; }
        public ICollection<NotaDebito> NotasDebito { get; set; }
        public ICollection<NotaCredito> NotasCredito { get; set; }
    }
}