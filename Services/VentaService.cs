using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sistema_ArgenMotos.DTOs;
using Sistema_ArgenMotos.Entidades;

namespace Sistema_ArgenMotos.Services
{
    public class VentaService : IVentaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VentaService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VentaDTO>> GetAllAsync()
        {
            var ventas = await _context.Ventas
                .Include(v => v.Articulos).ThenInclude(av => av.Articulo)
                .Include(v => v.Cliente)
                .Include(v => v.Vendedor)
                .Include(v => v.Factura).ThenInclude(f => f.Articulos).ThenInclude(fa => fa.Articulo)
                .Include(v => v.NotasDebito).ThenInclude(nd => nd.Articulos).ThenInclude(nda => nda.Articulo)
                .Include(v => v.NotasCredito).ThenInclude(nc => nc.Articulos).ThenInclude(nca => nca.Articulo)
                .ToListAsync();

            return _mapper.Map<IEnumerable<VentaDTO>>(ventas);
        }

        public async Task<VentaDTO> GetByIdAsync(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.Articulos).ThenInclude(av => av.Articulo)
                .Include(v => v.Cliente)
                .Include(v => v.Vendedor)
                .Include(v => v.Factura).ThenInclude(f => f.Articulos).ThenInclude(fa => fa.Articulo)
                .Include(v => v.NotasDebito).ThenInclude(nd => nd.Articulos).ThenInclude(nda => nda.Articulo)
                .Include(v => v.NotasCredito).ThenInclude(nc => nc.Articulos).ThenInclude(nca => nca.Articulo)
                .FirstOrDefaultAsync(v => v.VentaId == id);

            if (venta == null)
            {
                throw new Exception("Venta no encontrada");
            }

            return _mapper.Map<VentaDTO>(venta);
        }

        public async Task<IEnumerable<VentaDTO>> GetFilteredAsync(int? clienteId, int? vendedorId, decimal? precioMinimo, decimal? precioMaximo, DateTime? fechaMinima, DateTime? fechaMaxima, int? pageNumber, int? pageSize)
        {
            var query = _context.Ventas
                .Include(v => v.Articulos).ThenInclude(av => av.Articulo)
                .Include(v => v.Cliente)
                .Include(v => v.Vendedor)
                .Include(v => v.Factura).ThenInclude(f => f.Articulos).ThenInclude(fa => fa.Articulo)
                .Include(v => v.NotasDebito).ThenInclude(nd => nd.Articulos).ThenInclude(nda => nda.Articulo)
                .Include(v => v.NotasCredito).ThenInclude(nc => nc.Articulos).ThenInclude(nca => nca.Articulo)
                .AsQueryable();

            // Filtros opcionales
            if (clienteId.HasValue)
                query = query.Where(v => v.ClienteId == clienteId.Value);
            if (vendedorId.HasValue)
                query = query.Where(v => v.VendedorId == vendedorId.Value);
            if (precioMinimo.HasValue)
                query = query.Where(v => v.PrecioFinal >= precioMinimo.Value);
            if (precioMaximo.HasValue)
                query = query.Where(v => v.PrecioFinal <= precioMaximo.Value);
            if (fechaMinima.HasValue)
                query = query.Where(v => v.Fecha >= fechaMinima.Value);
            if (fechaMaxima.HasValue)
                query = query.Where(v => v.Fecha <= fechaMaxima.Value);

            // Paginación
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                int skip = (pageNumber.Value - 1) * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value);
            }

            var ventas = await query.ToListAsync();
            return _mapper.Map<IEnumerable<VentaDTO>>(ventas);
        }

        public async Task<VentaDTO> CreateAsync(VentaCreateUpdateDTO ventaDTO)
        {
            // Mapeo de DTO a entidad Venta
            var venta = new Venta
            {
                Fecha = DateTime.Now,
                ClienteId = ventaDTO.ClienteId,
                VendedorId = ventaDTO.VendedorId,
                PrecioFinal = ventaDTO.Articulos.Sum(a => a.PrecioUnitario * a.Cantidad),
                Articulos = ventaDTO.Articulos.Select(a => new Venta_Articulo
                {
                    ArticuloId = a.ArticuloId,
                    Cantidad = a.Cantidad,
                    PrecioUnitario = a.PrecioUnitario
                }).ToList()
            };

            // Agregar la venta a la base de datos
            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            // Crear la factura asociada
            var factura = new Factura
            {
                Fecha = DateTime.Now,
                VentaId = venta.VentaId,
                ClienteId = venta.ClienteId,
                VendedorId = venta.VendedorId,
                PrecioFinal = venta.PrecioFinal,
                Articulos = venta.Articulos.Select(va => new Factura_Articulo
                {
                    ArticuloId = va.ArticuloId,
                    Cantidad = va.Cantidad,
                    PrecioUnitario = va.PrecioUnitario
                }).ToList()
            };

            // Agregar la factura a la base de datos
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            // Asociar la factura a la venta y actualizar
            venta.Factura = factura;
            _context.Ventas.Update(venta);
            await _context.SaveChangesAsync();

            return _mapper.Map<VentaDTO>(venta);
        }

        public async Task<VentaDTO> UpdateAsync(int id, VentaCreateUpdateDTO ventaDTO)
        {
            // 1. Cargar la venta con sus artículos y relaciones necesarias
            var venta = await _context.Ventas
                .Include(v => v.Articulos)
                .ThenInclude(va => va.Articulo)
                .Include(v => v.Cliente)
                .Include(v => v.Vendedor)
                .FirstOrDefaultAsync(v => v.VentaId == id);

            if (venta == null)
            {
                throw new Exception("Venta no encontrada");
            }

            // 2. Guardar el precio anterior y lista de detalles modificados
            var precioAnterior = venta.PrecioFinal;
            decimal nuevoPrecioTotal = 0;
            var detallesModificados = new List<VentaArticuloDTO>();

            // 3. Restaurar stock antes de actualizar
            foreach (var ventaArticulo in venta.Articulos)
            {
                var articulo = await _context.Articulos.FindAsync(ventaArticulo.ArticuloId);
                if (articulo != null)
                {
                    articulo.StockActual += ventaArticulo.Cantidad;
                }
            }

            // 4. Limpiar artículos de la venta
            venta.Articulos.Clear();

            // 5. Procesar y comparar los artículos en la nueva venta
            foreach (var articuloDTO in ventaDTO.Articulos)
            {
                var articulo = await _context.Articulos.FindAsync(articuloDTO.ArticuloId);
                if (articulo == null)
                {
                    throw new Exception($"El artículo con ID {articuloDTO.ArticuloId} no existe");
                }

                if (articulo.StockActual < articuloDTO.Cantidad)
                {
                    throw new Exception($"Stock insuficiente para el artículo con ID {articuloDTO.ArticuloId}. Stock disponible: {articulo.StockActual}");
                }

                // Calcular el nuevo precio por artículo
                var nuevoPrecioArticulo = articuloDTO.Cantidad * articuloDTO.PrecioUnitario;
                nuevoPrecioTotal += nuevoPrecioArticulo;

                // Actualizar stock
                articulo.StockActual -= articuloDTO.Cantidad;

                // Crear la nueva relación venta-artículo
                var ventaArticulo = new Venta_Articulo
                {
                    ArticuloId = articulo.ArticuloId,
                    Cantidad = articuloDTO.Cantidad,
                    PrecioUnitario = articuloDTO.PrecioUnitario
                };
                venta.Articulos.Add(ventaArticulo);

                // Verificar cambios en precio o cantidad
                var detalleExistente = venta.Articulos
                    .FirstOrDefault(va => va.ArticuloId == articuloDTO.ArticuloId);
                if (detalleExistente == null || detalleExistente.PrecioUnitario != articuloDTO.PrecioUnitario || detalleExistente.Cantidad != articuloDTO.Cantidad)
                {
                    int difCantidad = 0;
                    decimal difPrecioUnitario = 0;
                    if (detalleExistente != null)
                    {
                        difCantidad = Math.Abs(articuloDTO.Cantidad - detalleExistente.Cantidad);
                        difPrecioUnitario = Math.Abs(articuloDTO.Cantidad - detalleExistente.Cantidad);

                    }
                    else
                    {
                        difCantidad = articuloDTO.Cantidad;
                        difPrecioUnitario = articuloDTO.Cantidad;

                    }

                    detallesModificados.Add(new VentaArticuloDTO
                    {
                        ArticuloId = articulo.ArticuloId,
                        Articulo = new ArticuloDTO { ArticuloId = articulo.ArticuloId, Descripcion = articulo.Descripcion },
                        Cantidad = difCantidad,
                        PrecioUnitario = difPrecioUnitario
                    });
                }
            }

            // 6. Actualizar el precio total de la venta
            venta.PrecioFinal = nuevoPrecioTotal;

            // 7. Generar notas de crédito o débito si el precio ha cambiado
            if (nuevoPrecioTotal < precioAnterior)
            {
                await GenerarNotaCredito(venta.VentaId, precioAnterior - nuevoPrecioTotal, detallesModificados);
            }
            else if (nuevoPrecioTotal > precioAnterior)
            {
                await GenerarNotaDebito(venta.VentaId, nuevoPrecioTotal - precioAnterior, detallesModificados);
            }

            _context.Ventas.Update(venta);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(venta.VentaId);
        }

        private async Task GenerarNotaCredito(int ventaId, decimal importe, List<VentaArticuloDTO> detalles)
        {
            var notaCredito = new NotaCredito
            {
                VentaId = ventaId,
                Importe = importe,
                Fecha = DateTime.Now,
                Articulos = detalles.Select(d => new NotaCredito_Articulo
                {
                    ArticuloId = d.ArticuloId,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario
                }).ToList()
            };

            _context.NotasCredito.Add(notaCredito);
            await _context.SaveChangesAsync();
        }

        private async Task GenerarNotaDebito(int ventaId, decimal importe, List<VentaArticuloDTO> detalles)
        {
            var notaDebito = new NotaDebito
            {
                VentaId = ventaId,
                Importe = importe,
                Fecha = DateTime.Now,
                Articulos = detalles.Select(d => new NotaDebito_Articulo
                {
                    ArticuloId = d.ArticuloId,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario
                }).ToList()
            };

            _context.NotasDebito.Add(notaDebito);
            await _context.SaveChangesAsync();
        }
    }
}
