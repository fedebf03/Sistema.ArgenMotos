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

        public async Task<FacturaDTO> CreateAsync(FacturaCreateUpdateDTO facturaDTO)
        {
            // Mapear el DTO a la entidad Factura sin artículos aún
            var factura = _mapper.Map<Factura>(facturaDTO);

            factura.Articulos = new List<Factura_Articulo>();
            decimal precioFinal = 0;

            foreach (var articuloDTO in facturaDTO.Articulos)
            {
                // Buscar el artículo en la base de datos
                var articulo = await _context.Articulos.FindAsync(articuloDTO.ArticuloId);

                if (articulo == null)
                {
                    throw new Exception($"El artículo con ID {articuloDTO.ArticuloId} no existe");
                }

                // Verificar si hay suficiente stock para vender
                if (articulo.StockActual < articuloDTO.Cantidad)
                {
                    throw new Exception($"Stock insuficiente para el artículo con ID {articuloDTO.ArticuloId}. Stock disponible: {articulo.StockActual}");
                }

                // Crear la relación Factura_Articulo y asignar el precio del artículo desde la base de datos
                var facturaArticulo = new Factura_Articulo
                {
                    ArticuloId = articulo.ArticuloId,
                    Cantidad = articuloDTO.Cantidad,
                    PrecioUnitario = articulo.Precio
                };

                // Calcular el precio total de la factura
                precioFinal += facturaArticulo.Cantidad * facturaArticulo.PrecioUnitario;

                // Actualizar el stock del artículo
                articulo.StockActual -= articuloDTO.Cantidad;

                // Añadir el artículo a la factura
                factura.Articulos.Add(facturaArticulo);
            }

            // Asignar el precio final calculado
            factura.PrecioFinal = precioFinal;

            // Guardar la factura y el stock actualizado
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            return _mapper.Map<FacturaDTO>(factura);
        }

        public async Task<FacturaDTO> UpdateAsync(int id, FacturaCreateUpdateDTO facturaDTO)
        {
            var factura = await _context.Facturas
                .Include(f => f.Articulos)
                .FirstOrDefaultAsync(f => f.FacturaId == id);

            if (factura == null)
            {
                throw new Exception("Factura no encontrada");
            }

            // Restaurar el stock de los artículos antes de la actualización
            foreach (var facturaArticulo in factura.Articulos)
            {
                var articulo = await _context.Articulos.FindAsync(facturaArticulo.ArticuloId);
                if (articulo != null)
                {
                    // Restaurar el stock anterior
                    articulo.StockActual += facturaArticulo.Cantidad;
                }
            }

            // Limpiar los artículos actuales para actualizar con los nuevos
            factura.Articulos.Clear();

            decimal precioFinal = 0;

            foreach (var articuloDTO in facturaDTO.Articulos)
            {
                // Buscar el artículo en la base de datos
                var articulo = await _context.Articulos.FindAsync(articuloDTO.ArticuloId);

                if (articulo == null)
                {
                    throw new Exception($"El artículo con ID {articuloDTO.ArticuloId} no existe");
                }

                // Verificar si hay suficiente stock para vender
                if (articulo.StockActual < articuloDTO.Cantidad)
                {
                    throw new Exception($"Stock insuficiente para el artículo con ID {articuloDTO.ArticuloId}. Stock disponible: {articulo.StockActual}");
                }

                // Crear la relación Factura_Articulo y asignar el precio del artículo desde la base de datos
                var facturaArticulo = new Factura_Articulo
                {
                    ArticuloId = articulo.ArticuloId,
                    Cantidad = articuloDTO.Cantidad,
                    PrecioUnitario = articulo.Precio
                };

                // Calcular el precio total de la factura
                precioFinal += facturaArticulo.Cantidad * facturaArticulo.PrecioUnitario;

                // Actualizar el stock del artículo
                articulo.StockActual -= articuloDTO.Cantidad;

                // Añadir el artículo a la factura
                factura.Articulos.Add(facturaArticulo);
            }

            // Asignar el precio final calculado
            factura.PrecioFinal = precioFinal;

            _context.Facturas.Update(factura);
            await _context.SaveChangesAsync();

            return _mapper.Map<FacturaDTO>(factura);
        }

        public async Task DeleteAsync(int id)
        {
            var factura = await _context.Facturas
                .Include(f => f.Articulos)
                .FirstOrDefaultAsync(f => f.FacturaId == id);

            if (factura == null)
            {
                throw new Exception("Factura no encontrada");
            }

            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();
        }
    }
}
