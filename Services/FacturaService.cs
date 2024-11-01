using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sistema_ArgenMotos.DTOs;
using Sistema_ArgenMotos.Entidades;

namespace Sistema_ArgenMotos.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FacturaService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FacturaDTO>> GetAllAsync()
        {
            var facturas = await _context.Facturas
                .Include(f => f.Articulos)
                .ThenInclude(fa => fa.Articulo)
                .ToListAsync();

            return _mapper.Map<IEnumerable<FacturaDTO>>(facturas);
        }

        public async Task<IEnumerable<FacturaDTO>> GetFilteredAsync(
            int? clienteId,
            int? vendedorId,
            decimal? precioMinimo,
            decimal? precioMaximo,
            DateTime? fechaMinima,
            DateTime? fechaMaxima,
            int? pageNumber,
            int? pageSize)
        {
            var query = _context.Facturas.AsQueryable();

            // Aplicar filtros
            if (clienteId.HasValue)
                query = query.Where(f => f.ClienteId == clienteId.Value);

            if (vendedorId.HasValue)
                query = query.Where(f => f.VendedorId == vendedorId.Value);

            if (precioMinimo.HasValue)
                query = query.Where(f => f.PrecioFinal >= precioMinimo.Value);

            if (precioMaximo.HasValue)
                query = query.Where(f => f.PrecioFinal <= precioMaximo.Value);

            if (fechaMinima.HasValue)
                query = query.Where(f => f.Fecha >= fechaMinima.Value);

            if (fechaMaxima.HasValue)
                query = query.Where(f => f.Fecha <= fechaMaxima.Value);

            // Aplicar paginación
            if (pageNumber.HasValue && pageSize.HasValue)
                query = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            var facturas = await query
                .Include(f => f.Cliente)
                .Include(f => f.Vendedor)
                .Include(f => f.Articulos)
                .ThenInclude(fa => fa.Articulo)
                .ToListAsync();

            return _mapper.Map<IEnumerable<FacturaDTO>>(facturas);
        }


        public async Task<FacturaDTO> GetByIdAsync(int id)
        {
            var factura = await _context.Facturas
                .Include(f => f.Articulos)
                .ThenInclude(fa => fa.Articulo)
                .FirstOrDefaultAsync(f => f.FacturaId == id);

            if (factura == null)
            {
                throw new Exception("Factura no encontrada");
            }

            return _mapper.Map<FacturaDTO>(factura);
        }
    }
}
