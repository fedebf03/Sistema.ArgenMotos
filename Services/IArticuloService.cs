using Sistema_ArgenMotos.DTOs;

namespace Sistema_ArgenMotos.Services
{
    public interface IArticuloService
    {
        Task<IEnumerable<ArticuloDTO>> GetAllAsync();
        Task<IEnumerable<ArticuloDTO>> GetFilteredAsync(string? nombre, string? marca, string? anno, string? descripcion, int? pageNumber, int? pageSize);
        Task<ArticuloDTO> GetByIdAsync(int id);
        Task<ArticuloDTO> AddAsync(ArticuloCreateUpdateDTO articuloCreateDto);
        Task<ArticuloDTO> UpdateAsync(int id, ArticuloCreateUpdateDTO articuloUpdateDto);
        Task DeleteAsync(int id);
    }
}
