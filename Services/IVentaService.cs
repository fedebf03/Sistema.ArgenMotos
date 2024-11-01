using Sistema_ArgenMotos.DTOs;

namespace Sistema_ArgenMotos.Services
{
    public interface IVentaService
    {
        Task<IEnumerable<VentaDTO>> GetAllAsync();
        Task<IEnumerable<VentaDTO>> GetFilteredAsync(int? clienteId, int? vendedorId, decimal? precioMinimo, decimal? precioMaximo, DateTime? fechaMinima, DateTime? fechaMaxima, int? pageNumber, int? pageSize);
        Task<VentaDTO> GetByIdAsync(int id);
        Task<VentaDTO> CreateAsync(VentaCreateUpdateDTO facturaDTO);
        Task<VentaDTO> UpdateAsync(int id, VentaCreateUpdateDTO facturaDTO);
    }
}
