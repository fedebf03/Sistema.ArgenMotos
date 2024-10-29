using Sistema_ArgenMotos.DTOs;
using Sistema_ArgenMotos.Enums;

namespace Sistema_ArgenMotos.Services
{
    public interface IVendedorService
    {
        Task<IEnumerable<VendedorDTO>> GetAllAsync();
        Task<IEnumerable<VendedorDTO>> GetFilteredAsync(string? dni, string? nombre, string? apellido, EstadoVendedor? estado, int? pageNumber, int? pageSize);
        Task<VendedorDTO> GetByIdAsync(int id);
        Task<VendedorDTO> AddAsync(VendedorCreateUpdateDTO vendedorDTO);
        Task<VendedorDTO> UpdateAsync(int id, VendedorCreateUpdateDTO vendedorDTO);
        Task DeleteAsync(int id);
    }
}
