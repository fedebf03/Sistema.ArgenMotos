using Sistema_ArgenMotos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sistema_ArgenMotos.Entidades
{
    public class Cobranza
    {
        [Key]
        public int CobranzaId { get; set; }

        [Required]
        public MetodoPago MetodoPago { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal MontoTotal { get; set; }

        [Required]
        public int VentaId { get; set; }
        public Venta Venta { get; set; }

        [Required]
        public DateTime Fecha { get; set; }
    }
}
