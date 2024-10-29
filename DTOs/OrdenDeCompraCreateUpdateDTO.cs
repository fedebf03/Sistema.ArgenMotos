using Sistema_ArgenMotos.Entidades;
using Sistema_ArgenMotos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sistema_ArgenMotos.DTOs
{
    public class OrdenDeCompraCreateUpdateDTO
    {
        [Required(ErrorMessage = "La fecha de la orden de compra es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El estado de la orden de compra aes obligatorio.")]
        public EstadoOrdenDeCompra Estado { get; set; }

        [Required(ErrorMessage = "El ID del proveedor es obligatorio.")]
        public int ProveedorId { get; set; }

        [Required(ErrorMessage = "La lista de artículos es obligatoria.")]
        public List<OrdenDeCompraCreateUpdateArticuloDTO> Articulos { get; set; }
    }

    public class OrdenDeCompraCreateUpdateArticuloDTO
    {
        [Required(ErrorMessage = "El ID del artículo es obligatorio.")]
        public int ArticuloId { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 1.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio unitario debe ser un valor positivo.")]
        public decimal PrecioUnitario { get; set; }
    }
}
