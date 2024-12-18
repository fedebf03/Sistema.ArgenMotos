﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Sistema_ArgenMotos.DTOs;
using Sistema_ArgenMotos.Services;

namespace Sistema_ArgenMotos.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturaService _facturaService;

        public FacturasController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaDTO>>> Get(
            [FromQuery] int? clienteId,
            [FromQuery] int? vendedorId,
            [FromQuery] decimal? precioMinimo,
            [FromQuery] decimal? precioMaximo,
            [FromQuery] DateTime? fechaMinima,
            [FromQuery] DateTime? fechaMaxima,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var facturas = await _facturaService.GetFilteredAsync(clienteId, vendedorId, precioMinimo, precioMaximo, fechaMinima, fechaMaxima, pageNumber, pageSize);
            return Ok(facturas);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<FacturaDTO>> Get(int id)
        {
            var factura = await _facturaService.GetByIdAsync(id);
            if (factura == null)
                return NotFound();
            return Ok(factura);
        }
    }
}
