namespace Sistema_ArgenMotos.DTOs
{
    public class NotaCreditoDTO
    {
        public int NotaCreditoId { get; set; }
        public int VentaId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Importe { get; set; }
        public int ClienteId { get; set; }
        public string VendedorId { get; set; }

        public List<NotaCreditoArticuloDTO> Articulos { get; set; }
    }

    public class NotaCreditoArticuloDTO
    {
        public int ArticuloId { get; set; }
        public ArticuloDTO Articulo { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
