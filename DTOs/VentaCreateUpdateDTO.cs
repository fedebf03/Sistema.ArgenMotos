using System.ComponentModel.DataAnnotations;

namespace Sistema_ArgenMotos.DTOs
{
    public class VentaCreateUpdateDTO
    {
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El ID del cliente es obligatorio.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El ID del vendedor es obligatorio.")]
        public int VendedorId { get; set; }

        [Required(ErrorMessage = "La lista de artículos es obligatoria.")]
        [MinLength(1, ErrorMessage = "Debe haber al menos un artículo en la venta.")]
        public List<VentaCreateUpdateArticuloDTO> Articulos { get; set; }
    }

    public class VentaCreateUpdateArticuloDTO
    {
        [Required]
        public int ArticuloId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser un valor mayor a 1.")]
        public int Cantidad { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El precio unitario debe ser un valor positivo.")]
        public decimal PrecioUnitario { get; set; }
    }
}
