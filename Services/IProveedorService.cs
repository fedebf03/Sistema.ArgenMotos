using Sistema_ArgenMotos.DTOs;
using Sistema_ArgenMotos.Enums;

namespace Sistema_ArgenMotos.Services
{
    public interface IProveedorService
    {
        Task<IEnumerable<ProveedorDTO>> GetAllAsync();
        Task<IEnumerable<ProveedorDTO>> GetFilteredAsync(string? nombre, string? cuil, EstadoProveedor? estado, int? pageNumber, int? pageSize);
        Task<ProveedorDTO> GetByIdAsync(int id);
        Task<ProveedorDTO> CreateAsync(ProveedorCreateUpdateDTO proveedorCreateDto);
        Task<ProveedorDTO> UpdateAsync(int id, ProveedorCreateUpdateDTO proveedorUpdateDto);
        Task DeleteAsync(int id);
    }
}
