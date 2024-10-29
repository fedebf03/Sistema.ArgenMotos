using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sistema_ArgenMotos.DTOs;
using Sistema_ArgenMotos.Entidades;

namespace Sistema_ArgenMotos.Services
{
    public class OrdenDeCompraService : IOrdenDeCompraService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrdenDeCompraService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrdenDeCompraDTO>> GetAllAsync()
        {
            var ordenesDeCompra = await _context.OrdenesDeCompra
                .Include(o => o.Articulos)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrdenDeCompraDTO>>(ordenesDeCompra);
        }

        public async Task<IEnumerable<OrdenDeCompraDTO>> GetFilteredAsync(
            int? proveedorId,
            EstadoOrdenDeCompra? estado,
            decimal? precioMinimo,
            decimal? precioMaximo,
            DateTime? fechaMinima,
            DateTime? fechaMaxima,
            int? pageNumber,
            int? pageSize)
        {
            var query = _context.OrdenesDeCompra.AsQueryable();

            // Aplicar filtros
            if (proveedorId.HasValue)
                query = query.Where(o => o.ProveedorId == proveedorId.Value);

            if (estado.HasValue)
                query = query.Where(o => o.Estado == estado);

            if (precioMinimo.HasValue)
                query = query.Where(o => o.PrecioTotal >= precioMinimo.Value);

            if (precioMaximo.HasValue)
                query = query.Where(o => o.PrecioTotal <= precioMaximo.Value);

            if (fechaMinima.HasValue)
                query = query.Where(o => o.Fecha >= fechaMinima.Value);

            if (fechaMaxima.HasValue)
                query = query.Where(o => o.Fecha <= fechaMaxima.Value);

            // Aplicar paginación
            if (pageNumber.HasValue && pageSize.HasValue)
                query = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            var ordenes = await query
                .Include(o => o.Proveedor)
                .Include(o => o.Articulos)
                .ThenInclude(oa => oa.Articulo)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrdenDeCompraDTO>>(ordenes);
        }


        public async Task<OrdenDeCompraDTO> GetByIdAsync(int id)
        {
            var ordenDeCompra = await _context.OrdenesDeCompra
                .Include(o => o.Articulos)
                .FirstOrDefaultAsync(o => o.OrdenDeCompraId == id);

            if (ordenDeCompra == null)
            {
                throw new Exception("Orden de compra no encontrada");
            }

            return _mapper.Map<OrdenDeCompraDTO>(ordenDeCompra);
        }

        public async Task<OrdenDeCompraDTO> AddAsync(OrdenDeCompraCreateUpdateDTO ordenDeCompraDTO)
        {
            var ordenDeCompra = _mapper.Map<OrdenDeCompra>(ordenDeCompraDTO);
            ordenDeCompra.Articulos = new List<OrdenDeCompra_Articulo>();
            decimal precioTotal = 0;

            foreach (var articuloDTO in ordenDeCompraDTO.Articulos)
            {
                // Buscar el artículo en la base de datos para validar su existencia
                var articulo = await _context.Articulos.FindAsync(articuloDTO.ArticuloId);
                if (articulo == null)
                {
                    throw new Exception($"El artículo con ID {articuloDTO.ArticuloId} no existe");
                }

                // Crear la relación OrdenDeCompra_Articulo usando el PrecioUnitario proporcionado por el DTO
                var ordenArticulo = new OrdenDeCompra_Articulo
                {
                    ArticuloId = articulo.ArticuloId,
                    Cantidad = articuloDTO.Cantidad,
                    PrecioUnitario = articuloDTO.PrecioUnitario // Usar el precio unitario de compra proporcionado en el DTO
                };

                // Calcular el precio total de la orden
                precioTotal += ordenArticulo.Cantidad * ordenArticulo.PrecioUnitario;

                // Añadir el artículo a la orden de compra
                ordenDeCompra.Articulos.Add(ordenArticulo);
            }

            // Asignar el precio total calculado
            ordenDeCompra.PrecioTotal = precioTotal;

            _context.OrdenesDeCompra.Add(ordenDeCompra);
            await _context.SaveChangesAsync();

            // Actualizar stock si la orden está completa
            if (ordenDeCompra.Estado == EstadoOrdenDeCompra.Completa)
            {
                await ActualizarStockArticulos(ordenDeCompra.Articulos);
            }

            return _mapper.Map<OrdenDeCompraDTO>(ordenDeCompra);
        }

        public async Task<OrdenDeCompraDTO> UpdateAsync(int id, OrdenDeCompraCreateUpdateDTO ordenDeCompraDTO)
        {
            var ordenDeCompra = await _context.OrdenesDeCompra
                .Include(o => o.Articulos)
                .FirstOrDefaultAsync(o => o.OrdenDeCompraId == id);

            if (ordenDeCompra == null)
            {
                throw new Exception("Orden de compra no encontrada");
            }

            // Verificar si la orden ya está aprobada o completa
            if (ordenDeCompra.Estado == EstadoOrdenDeCompra.Aprobada ||
                ordenDeCompra.Estado == EstadoOrdenDeCompra.Completa)
            {
                throw new Exception("No se puede modificar una orden de compra que ya está aprobada o completa.");
            }

            // Actualizar los campos de la orden de compra
            ordenDeCompra.Fecha = ordenDeCompraDTO.Fecha;
            ordenDeCompra.ProveedorId = ordenDeCompraDTO.ProveedorId;

            // Limpiar los artículos actuales para actualizar con los nuevos
            ordenDeCompra.Articulos.Clear();

            decimal precioTotal = 0;

            foreach (var articuloDTO in ordenDeCompraDTO.Articulos)
            {
                // Buscar el artículo en la base de datos para validar su existencia
                var articulo = await _context.Articulos.FindAsync(articuloDTO.ArticuloId);
                if (articulo == null)
                {
                    throw new Exception($"El artículo con ID {articuloDTO.ArticuloId} no existe");
                }

                // Crear la relación OrdenDeCompra_Articulo usando el PrecioUnitario proporcionado por el DTO
                var ordenArticulo = new OrdenDeCompra_Articulo
                {
                    ArticuloId = articulo.ArticuloId,
                    Cantidad = articuloDTO.Cantidad,
                    PrecioUnitario = articuloDTO.PrecioUnitario // Usar el precio unitario de compra proporcionado en el DTO
                };

                // Calcular el precio total de la orden
                precioTotal += ordenArticulo.Cantidad * ordenArticulo.PrecioUnitario;

                // Añadir el artículo a la orden de compra
                ordenDeCompra.Articulos.Add(ordenArticulo);
            }

            // Asignar el precio total calculado
            ordenDeCompra.PrecioTotal = precioTotal;

            // Actualizar el estado si el DTO tiene un nuevo estado
            var estadoAnterior = ordenDeCompra.Estado;
            ordenDeCompra.Estado = ordenDeCompraDTO.Estado;

            // Guardar los cambios en la base de datos
            _context.OrdenesDeCompra.Update(ordenDeCompra);
            await _context.SaveChangesAsync();

            // Si el estado de la orden cambió a "Completada", actualizar el stock de los artículos
            if (ordenDeCompra.Estado == EstadoOrdenDeCompra.Completa && estadoAnterior != EstadoOrdenDeCompra.Completa)
            {
                await ActualizarStockArticulos(ordenDeCompra.Articulos);
            }

            return _mapper.Map<OrdenDeCompraDTO>(ordenDeCompra);
        }

        public async Task DeleteAsync(int id)
        {
            var ordenDeCompra = await _context.OrdenesDeCompra
                .Include(o => o.Articulos)
                .FirstOrDefaultAsync(o => o.OrdenDeCompraId == id);

            if (ordenDeCompra == null)
            {
                throw new Exception("Orden de compra no encontrada");
            }

            _context.OrdenesDeCompra.Remove(ordenDeCompra);
            await _context.SaveChangesAsync();
        }

        private async Task ActualizarStockArticulos(ICollection<OrdenDeCompra_Articulo> ordenDeCompraArticulos)
        {
            foreach (var ordenArticulo in ordenDeCompraArticulos)
            {
                var articulo = await _context.Articulos.FindAsync(ordenArticulo.ArticuloId);
                if (articulo != null)
                {
                    // Actualizar el stock
                    articulo.StockActual += ordenArticulo.Cantidad;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
