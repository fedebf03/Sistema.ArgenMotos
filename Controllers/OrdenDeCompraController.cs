using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_ArgenMotos.DTOs;
using Sistema_ArgenMotos.Entidades;
using Sistema_ArgenMotos.Services;

namespace Sistema_ArgenMotos.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrdenDeCompraController : ControllerBase
    {
        private readonly IOrdenDeCompraService _ordenDeCompraService;

        public OrdenDeCompraController(IOrdenDeCompraService ordenDeCompraService)
        {
            _ordenDeCompraService = ordenDeCompraService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenDeCompraDTO>>> Get(
            [FromQuery] int? proveedorId,
            [FromQuery] EstadoOrdenDeCompra? estado,
            [FromQuery] decimal? precioMinimo,
            [FromQuery] decimal? precioMaximo,
            [FromQuery] DateTime? fechaMinima,
            [FromQuery] DateTime? fechaMaxima,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var ordenes = await _ordenDeCompraService.GetFilteredAsync(proveedorId, estado, precioMinimo, precioMaximo, fechaMinima, fechaMaxima, pageNumber, pageSize);
            return Ok(ordenes);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenDeCompraDTO>> GetById(int id)
        {
            var orden = await _ordenDeCompraService.GetByIdAsync(id);
            return Ok(orden);
        }

        [HttpPost]
        public async Task<ActionResult<OrdenDeCompraDTO>> Create(OrdenDeCompraCreateUpdateDTO ordenDTO)
        {
            var orden = await _ordenDeCompraService.AddAsync(ordenDTO);
            return Ok(orden);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrdenDeCompraDTO>> Update(int id, OrdenDeCompraCreateUpdateDTO ordenDTO)
        {
            var orden = await _ordenDeCompraService.UpdateAsync(id, ordenDTO);
            return Ok(orden);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _ordenDeCompraService.DeleteAsync(id);
            return NoContent();
        }
    }
}