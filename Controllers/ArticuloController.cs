using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Sistema_ArgenMotos.DTOs;
using Sistema_ArgenMotos.Services;

namespace Sistema_ArgenMotos.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ArticuloController : ControllerBase
    {
        private readonly IArticuloService _articuloService;

        public ArticuloController(IArticuloService articuloService)
        {
            _articuloService = articuloService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticuloDTO>>> Get(
            [FromQuery] string? nombre,
            [FromQuery] string? marca,
            [FromQuery] string? anno,
            [FromQuery] string? descripcion,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var articulos = await _articuloService.GetFilteredAsync(nombre, marca, anno, descripcion, pageNumber, pageSize);
            return Ok(articulos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticuloDTO>> Get(int id)
        {
            var articulo = await _articuloService.GetByIdAsync(id);
            return Ok(articulo);
        }

        [HttpPost]
        public async Task<ActionResult<ArticuloDTO>> Create([FromBody] ArticuloCreateUpdateDTO articuloCreateDto)
        {
            var articulo = await _articuloService.AddAsync(articuloCreateDto);
            return CreatedAtAction(nameof(Get), new { id = articulo.ArticuloId }, articulo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ArticuloDTO>> Update(int id, [FromBody] ArticuloCreateUpdateDTO articuloUpdateDto)
        {
            var articulo = await _articuloService.UpdateAsync(id, articuloUpdateDto);
            return Ok(articulo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _articuloService.DeleteAsync(id);
            return NoContent();
        }
    }
}