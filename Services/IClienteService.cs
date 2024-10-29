using Sistema_ArgenMotos.DTOs;
using Sistema_ArgenMotos.Enums;

namespace Sistema_ArgenMotos.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDTO>> GetAllAsync();
        Task<IEnumerable<ClienteDTO>> GetFilteredAsync(string? nombre, string? apellido, string? dni, TipoCliente? tipo, int? pageNumber, int? pageSize);
        Task<ClienteDTO> GetByIdAsync(int id);
        Task<ClienteDTO> CreateAsync(ClienteCreateUpdateDTO clienteDto);
        Task<ClienteDTO> UpdateAsync(int id, ClienteCreateUpdateDTO clienteDto);
        Task DeleteAsync(int id);
    }
}
