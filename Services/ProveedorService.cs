using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sistema_ArgenMotos.DTOs;
using Sistema_ArgenMotos.Entidades;
using Sistema_ArgenMotos.Enums;

namespace Sistema_ArgenMotos.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProveedorService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProveedorDTO>> GetAllAsync()
        {
            var proveedores = await _context.Proveedores.ToListAsync();
            return _mapper.Map<IEnumerable<ProveedorDTO>>(proveedores);
        }

        public async Task<IEnumerable<ProveedorDTO>> GetFilteredAsync(
            string? nombre,
            string? cuil,
            EstadoProveedor? estado,
            int? pageNumber,
            int? pageSize)
        {
            var query = _context.Proveedores.AsQueryable();

            // Aplicar filtros
            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(p => p.Nombre.Contains(nombre));

            if (!string.IsNullOrEmpty(cuil))
                query = query.Where(p => p.CUIL.Contains(cuil));

            if (estado.HasValue)
            {
                query = query.Where(p => p.Estado == estado);
            }

            // Aplicar paginación
            if (pageNumber.HasValue && pageSize.HasValue)
                query = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            var proveedores = await query
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProveedorDTO>>(proveedores);
        }


        public async Task<ProveedorDTO> GetByIdAsync(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            return _mapper.Map<ProveedorDTO>(proveedor);
        }

        public async Task<ProveedorDTO> CreateAsync(ProveedorCreateUpdateDTO proveedorCreateDto)
        {
            var proveedor = _mapper.Map<Proveedor>(proveedorCreateDto);
            var addedProveedor = _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProveedorDTO>(addedProveedor.Entity);
        }

        public async Task<ProveedorDTO> UpdateAsync(int id, ProveedorCreateUpdateDTO proveedorUpdateDto)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
            {
                throw new Exception("Proveedor no encontrado");
            }

            _mapper.Map(proveedorUpdateDto, proveedor);
            var updatedProveedor = _context.Proveedores.Update(proveedor);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProveedorDTO>(updatedProveedor.Entity);
        }

        public async Task DeleteAsync(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
            {
                throw new Exception("Cliente no encontrado");
            }

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();
        }
    }
}
