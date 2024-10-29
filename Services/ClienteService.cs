using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sistema_ArgenMotos.DTOs;
using Sistema_ArgenMotos.Entidades;
using Sistema_ArgenMotos.Enums;

namespace Sistema_ArgenMotos.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClienteService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteDTO>> GetAllAsync()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
        }

        public async Task<IEnumerable<ClienteDTO>> GetFilteredAsync(
            string? nombre,
            string? apellido,
            string? dni,
            TipoCliente? tipo,
            int? pageNumber,
            int? pageSize)
        {
            var query = _context.Clientes.AsQueryable();

            // Aplicar filtros
            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(c => c.Nombre.Contains(nombre));

            if (!string.IsNullOrEmpty(apellido))
                query = query.Where(c => c.Apellido.Contains(apellido));

            if (!string.IsNullOrEmpty(dni))
                query = query.Where(c => c.DNI.Contains(dni));

            if (tipo.HasValue)
                query = query.Where(c => c.Tipo == tipo.Value);

            // Aplicar paginación
            if (pageNumber.HasValue && pageSize.HasValue)
                query = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            var clientes = await query
                .ToListAsync();

            return _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
        }

        public async Task<ClienteDTO> GetByIdAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            return _mapper.Map<ClienteDTO>(cliente);
        }

        public async Task<ClienteDTO> CreateAsync(ClienteCreateUpdateDTO clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);
            var createdCliente = _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClienteDTO>(createdCliente.Entity);
        }

        public async Task<ClienteDTO> UpdateAsync(int id, ClienteCreateUpdateDTO clienteDTO)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                throw new Exception("Cliente no encontrado");
            }

            _mapper.Map(clienteDTO, cliente);
            var updatedCliente = _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClienteDTO>(updatedCliente.Entity);
        }

        public async Task DeleteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                throw new Exception("Cliente no encontrado");
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
