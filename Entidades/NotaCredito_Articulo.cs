using System.ComponentModel.DataAnnotations;

namespace Sistema_ArgenMotos.Entidades
{
    public class NotaCredito_Articulo
    {
        [Key]
        public int NotaCreditoId { get; set; }
        public NotaCredito NotaCredito { get; set; }

        [Key]
        public int ArticuloId { get; set; }
        public Articulo Articulo { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Cantidad { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PrecioUnitario { get; set; }
    }
}
