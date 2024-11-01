namespace Sistema_ArgenMotos.DTOs
{
    public class FacturaDTO
    {
        public int FacturaId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal PrecioFinal { get; set; }
        public ClienteDTO Cliente { get; set; }
        public VendedorDTO VendedorDTO { get; set; }
        public List<FacturaArticuloDTO> Articulos { get; set; }
    }

    public class FacturaArticuloDTO
    {
        public int ArticuloId { get; set; }
        public ArticuloDTO Articulo { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
