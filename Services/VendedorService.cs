using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sistema_ArgenMotos.DTOs;
using Sistema_ArgenMotos.Entidades;
using Sistema_ArgenMotos.Enums;

namespace Sistema_ArgenMotos.Services
{
    public class VendedorService : IVendedorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VendedorService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VendedorDTO>> GetAllAsync()
        {
            var vendedores = await _context.Vendedores.ToListAsync();
            return _mapper.Map<IEnumerable<VendedorDTO>>(vendedores);
        }

        public async Task<IEnumerable<VendedorDTO>> GetFilteredAsync(
            string? dni,
            string? nombre,
            string? apellido,
            EstadoVendedor? estado,
            int? pageNumber,
            int? pageSize)
        {
            var query = _context.Vendedores.AsQueryable();

            // Aplicar filtros
            if (!string.IsNullOrEmpty(dni))
                query = query.Where(v => v.DNI.Contains(dni));

            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(v => v.Nombre.Contains(nombre));

            if (!string.IsNullOrEmpty(apellido))
                query = query.Where(v => v.Apellido.Contains(apellido));

            if (estado.HasValue)
            {
                query = query.Where(v => v.Estado == estado);
            }

            // Aplicar paginación
            if (pageNumber.HasValue && pageSize.HasValue)
                query = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            var vendedores = await query
                .ToListAsync();

            return _mapper.Map<IEnumerable<VendedorDTO>>(vendedores);
        }


        public async Task<VendedorDTO> GetByIdAsync(int id)
        {
            var vendedor = await _context.Vendedores.FindAsync(id);

            if (vendedor == null)
            {
                throw new Exception("Vendedor not found");
            }

            return _mapper.Map<VendedorDTO>(vendedor);
        }

        public async Task<VendedorDTO> AddAsync(VendedorCreateUpdateDTO vendedorDTO)
        {
            var vendedorExistente = await _context.Vendedores.FirstOrDefaultAsync(v => v.Email == vendedorDTO.Email);

            if (vendedorExistente != null)
            {
                throw new Exception($"Ya existe un vendedor con email {vendedorDTO.Email}");
            }

            var vendedor = _mapper.Map<Vendedor>(vendedorDTO);
            var newVendedor = _context.Vendedores.Add(vendedor);
            await _context.SaveChangesAsync();

            return _mapper.Map<VendedorDTO>(newVendedor.Entity);
        }

        public async Task<VendedorDTO> UpdateAsync(int id, VendedorCreateUpdateDTO vendedorDTO)
        {
            var vendedor = await _context.Vendedores.FindAsync(id);

            if (vendedor == null)
            {
                throw new Exception("Vendedor not found");
            }

            _mapper.Map(vendedorDTO, vendedor);
            var updatedVendedor = _context.Vendedores.Update(vendedor);
            await _context.SaveChangesAsync();

            return _mapper.Map<VendedorDTO>(updatedVendedor.Entity);
        }

        public async Task DeleteAsync(int id)
        {
            var vendedor = await _context.Vendedores.FindAsync(id);

            if (vendedor == null)
            {
                throw new Exception("Vendedor not found");
            }

            _context.Vendedores.Remove(vendedor);
            await _context.SaveChangesAsync();
        }
    }
}
