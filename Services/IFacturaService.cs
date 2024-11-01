using Sistema_ArgenMotos.DTOs;

namespace Sistema_ArgenMotos.Services
{
    public interface IFacturaService
    {
        Task<IEnumerable<FacturaDTO>> GetAllAsync();
        Task<IEnumerable<FacturaDTO>> GetFilteredAsync(int? clienteId, int? vendedorId, decimal? precioMinimo, decimal? precioMaximo, DateTime? fechaMinima, DateTime? fechaMaxima, int? pageNumber, int? pageSize);
        Task<FacturaDTO> GetByIdAsync(int id);
    }
}
