using Sistema_ArgenMotos.DTOs;
using Sistema_ArgenMotos.Entidades;

namespace Sistema_ArgenMotos.Services
{
    public interface IOrdenDeCompraService
    {
        Task<IEnumerable<OrdenDeCompraDTO>> GetAllAsync();
        Task<IEnumerable<OrdenDeCompraDTO>> GetFilteredAsync(int? proveedorId, EstadoOrdenDeCompra? estado, decimal? precioMinimo, decimal? precioMaximo, DateTime? fechaMinima, DateTime? fechaMaxima, int? pageNumber, int? pageSize);
        Task<OrdenDeCompraDTO> GetByIdAsync(int id);
        Task<OrdenDeCompraDTO> AddAsync(OrdenDeCompraCreateUpdateDTO ordenDeCompraCreateDto);
        Task<OrdenDeCompraDTO> UpdateAsync(int id, OrdenDeCompraCreateUpdateDTO ordenDeCompraUpdateDto);
        Task DeleteAsync(int id);
    }
}
