using Microsoft.AspNetCore.Routing.Constraints;
using System.ComponentModel.DataAnnotations;

namespace Sistema_ArgenMotos.DTOs
{
    public class FacturaCreateUpdateDTO
    {
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El ID del cliente es obligatorio.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El ID del vendedor es obligatorio.")]
        public int VendedorId { get; set; }

        [Required(ErrorMessage = "La lista de artículos es obligatoria.")]
        [MinLength(1, ErrorMessage = "Debe haber al menos un artículo en la factura.")]
        public List<FacturaCreateUpdateArticuloDTO> Articulos { get; set; }
    }

    public class FacturaCreateUpdateArticuloDTO
    {
        [Required]
        public int ArticuloId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser un valor mayor a 1.")]
        public int Cantidad { get; set; }
    }
}
