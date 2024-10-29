using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sistema_ArgenMotos.DTOs;
using Sistema_ArgenMotos.Entidades;

namespace Sistema_ArgenMotos.Services
{
    public class ArticuloService : IArticuloService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ArticuloService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ArticuloDTO>> GetAllAsync()
        {
            var articulos = await _context.Articulos.ToListAsync();
            return _mapper.Map<IEnumerable<ArticuloDTO>>(articulos);
        }

        public async Task<IEnumerable<ArticuloDTO>> GetFilteredAsync(
            string? nombre,
            string? marca,
            string? anno,
            string? descripcion,
            int? pageNumber,
            int? pageSize)
        {
            var query = _context.Articulos.AsQueryable();

            // Aplicar filtros
            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(a => a.Modelo.Contains(nombre));

            if (!string.IsNullOrEmpty(marca))
                query = query.Where(a => a.Marca.Contains(marca));

            if (!string.IsNullOrEmpty(anno))
                query = query.Where(a => a.Anno == anno);

            if (!string.IsNullOrEmpty(descripcion))
                query = query.Where(a => a.Descripcion.Contains(descripcion));

            // Aplicar paginación
            if (pageNumber.HasValue && pageSize.HasValue)
                query = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            var articulos = await query
                .ToListAsync();

            return _mapper.Map<IEnumerable<ArticuloDTO>>(articulos);
        }

        public async Task<ArticuloDTO> GetByIdAsync(int id)
        {
            var articulo = await _context.Articulos.FindAsync(id);
            return _mapper.Map<ArticuloDTO>(articulo);
        }

        public async Task<ArticuloDTO> AddAsync(ArticuloCreateUpdateDTO articuloCreateDto)
        {
            var articulo = _mapper.Map<Articulo>(articuloCreateDto);

            var addedArticulo = _context.Articulos.Add(articulo);
            await _context.SaveChangesAsync();

            return _mapper.Map<ArticuloDTO>(addedArticulo.Entity);
        }

        public async Task<ArticuloDTO> UpdateAsync(int id, ArticuloCreateUpdateDTO articuloUpdateDto)
        {
            var articulo = await _context.Articulos.FindAsync(id);

            if (articulo == null)
            {
                throw new Exception("Articulo not found");
            }

            _mapper.Map(articuloUpdateDto, articulo);
            var updatedArticulo = _context.Articulos.Update(articulo);
            await _context.SaveChangesAsync();

            return _mapper.Map<ArticuloDTO>(updatedArticulo.Entity);
        }

        public async Task DeleteAsync(int id)
        {
            var articulo = await _context.Articulos.FindAsync(id);

            if (articulo == null)
            {
                throw new Exception("Articulo not found");
            }

            _context.Articulos.Remove(articulo);
            await _context.SaveChangesAsync();
        }
    }
}
