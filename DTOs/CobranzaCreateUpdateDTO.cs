using Sistema_ArgenMotos.Entidades;
using Sistema_ArgenMotos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sistema_ArgenMotos.DTOs
{
    public class CobranzaCreateUpdateDTO
    {
        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        public MetodoPago MetodoPago { get; set; }

        [Required(ErrorMessage = "El ID de la factura es obligatorio.")]
        public int FacturaId { get; set; }

        [Required(ErrorMessage = "La fecha de cobranza es obligatoria.")]
        public DateTime FechaCobranza { get; set; }
    }
}