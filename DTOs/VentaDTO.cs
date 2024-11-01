namespace Sistema_ArgenMotos.DTOs
{
    public class VentaDTO
    {
        public int VentaId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal PrecioFinal { get; set; }
        public ClienteDTO Cliente { get; set; }
        public VendedorDTO Vendedor { get; set; }
        public List<VentaArticuloDTO> Articulos { get; set; }

        public FacturaDTO Factura { get; set; }
        public List<NotaDebitoDTO> NotasDebito { get; set; }
        public List<NotaCreditoDTO> NotasCredito { get; set; }
    }

    public class VentaArticuloDTO
    { 
        public int ArticuloId { get; set; }
        public ArticuloDTO Articulo { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
