using Sistema_ArgenMotos.Enums;

namespace Sistema_ArgenMotos.DTOs
{
    public class VendedorDTO
    {
        public int VendedorId { get; set; }
        public string DNI { get; set; }
        public string Legajo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public EstadoVendedor Estado { get; set; }
    }
}
