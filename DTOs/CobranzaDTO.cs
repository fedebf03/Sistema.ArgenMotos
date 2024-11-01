using Sistema_ArgenMotos.Enums;

namespace Sistema_ArgenMotos.DTOs
{
    public class CobranzaDTO
    {
        public int CobranzaId { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public decimal MontoTotal { get; set; }
        public int VentaId { get; set; }
        public DateTime Fecha { get; set; }
    }
}
