using Sistema_ArgenMotos.Enums;

namespace Sistema_ArgenMotos.DTOs
{
    public class ProveedorDTO
    {
        public int ProveedorId { get; set; }
        public string RazonSocial { get; set; }
        public string Nombre { get; set; }
        public string CUIL { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public EstadoProveedor Estado { get; set; }
    }
}
