using Microsoft.AspNetCore.Mvc;
using Sistema_ArgenMotos.DTOs;
using Sistema_ArgenMotos.Entidades;
using Sistema_ArgenMotos.Enums;
using Sistema_ArgenMotos.Services;

namespace Sistema_ArgenMotos.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> Get(
            [FromQuery] string? nombre,
            [FromQuery] string? apellido,
            [FromQuery] string? dni,
            [FromQuery] TipoCliente? tipo,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var clientes = await _clienteService.GetFilteredAsync(nombre, apellido, dni, tipo, pageNumber, pageSize);
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> Get(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> Create(ClienteCreateUpdateDTO clienteDto)
        {
            var newCliente = await _clienteService.CreateAsync(clienteDto);
            return CreatedAtAction(nameof(Get), new { id = newCliente.ClienteId }, newCliente);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteDTO>> Update(int id, ClienteCreateUpdateDTO clienteDto)
        {
            var updatedCliente = await _clienteService.UpdateAsync(id, clienteDto);
            return Ok(updatedCliente);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _clienteService.DeleteAsync(id);
            return NoContent();
        }
    }
}
