using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Sistema_ArgenMotos.DTOs;
using Sistema_ArgenMotos.Services;

namespace Sistema_ArgenMotos.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentasController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VentaDTO>>> Get(
            [FromQuery] int? clienteId,
            [FromQuery] int? vendedorId,
            [FromQuery] decimal? precioMinimo,
            [FromQuery] decimal? precioMaximo,
            [FromQuery] DateTime? fechaMinima,
            [FromQuery] DateTime? fechaMaxima,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var ventas = await _ventaService.GetFilteredAsync(clienteId, vendedorId, precioMinimo, precioMaximo, fechaMinima, fechaMaxima, pageNumber, pageSize);
            return Ok(ventas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VentaDTO>> Get(int id)
        {
            var venta = await _ventaService.GetByIdAsync(id);
            if (venta == null)
                return NotFound();
            return Ok(venta);
        }

        [HttpPost]
        public async Task<ActionResult<VentaDTO>> Create(VentaCreateUpdateDTO ventaDTO)
        {
            var venta = await _ventaService.CreateAsync(ventaDTO);
            return CreatedAtAction(nameof(Get), new { id = venta.VentaId }, ventaDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VentaDTO>> Update(int id, VentaCreateUpdateDTO ventaDTO)
        {
            var venta = await _ventaService.UpdateAsync(id, ventaDTO);
            return Ok(venta);
        }
    }
}
