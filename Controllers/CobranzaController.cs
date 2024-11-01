using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Sistema_ArgenMotos.DTOs;
using Sistema_ArgenMotos.Entidades;
using Sistema_ArgenMotos.Enums;
using Sistema_ArgenMotos.Services;

namespace Sistema_ArgenMotos.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CobranzasController : ControllerBase
    {
        private readonly ICobranzaService _cobranzaService;

        public CobranzasController(ICobranzaService cobranzaService)
        {
            _cobranzaService = cobranzaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CobranzaDTO>>> Get(
            [FromQuery] MetodoPago? metodoPago,
            [FromQuery] decimal? montoMinimo,
            [FromQuery] decimal? montoMaximo,
            [FromQuery] DateTime? fechaCobranzaMinima,
            [FromQuery] DateTime? fechaCobranzaMaxima,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var cobranzas = await _cobranzaService.GetFilteredAsync(metodoPago, montoMinimo, montoMaximo, fechaCobranzaMinima, fechaCobranzaMaxima, pageNumber, pageSize);
            return Ok(cobranzas);
        }


        [HttpPost]
        public async Task<ActionResult<CobranzaDTO>> Create(CobranzaCreateUpdateDTO cobranzaDTO)
        {
            var cobranza = await _cobranzaService.CreateAsync(cobranzaDTO);
            return CreatedAtAction(nameof(Get), new { id = cobranza.CobranzaId }, cobranza);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CobranzaDTO>> Update(int id, CobranzaCreateUpdateDTO cobranzaDTO)
        {
            var cobranza = await _cobranzaService.UpdateAsync(id, cobranzaDTO);
            return Ok(cobranza);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _cobranzaService.DeleteAsync(id);
            return NoContent();
        }
    }
}
