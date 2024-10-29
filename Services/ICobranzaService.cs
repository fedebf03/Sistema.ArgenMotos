using Sistema_ArgenMotos.DTOs;
using Sistema_ArgenMotos.Enums;

namespace Sistema_ArgenMotos.Services
{
    public interface ICobranzaService
    {
        Task<IEnumerable<CobranzaDTO>> GetAllAsync();
        Task<IEnumerable<CobranzaDTO>> GetFilteredAsync(MetodoPago? metodoPago, decimal? montoMinimo, decimal? montoMaximo, DateTime? fechaCobranzaMinima, DateTime? fechaCobranzaMaxima, int? pageNumber, int? pageSize);
        Task<CobranzaDTO> GetByIdAsync(int id);
        Task<CobranzaDTO> CreateAsync(CobranzaCreateUpdateDTO cobranzaDTO);
        Task<CobranzaDTO> UpdateAsync(int id, CobranzaCreateUpdateDTO cobranzaDTO);
        Task DeleteAsync(int id);
    }
}
