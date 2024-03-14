using Apptenlink_Back.Entities;
using Apptenlink_Back.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apptenlink_Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService ?? throw new ArgumentNullException(nameof(clienteService));
        }

        [HttpGet("ConsultarTodos")]
        public async Task<ActionResult<IEnumerable<Cliente>>> ListarTodosClientesAsync()
        {
            var clientes = await _clienteService.ListarTodosClientesAsync();
            return Ok(clientes);
        }

        [HttpGet("ConsultarPorId/{id}")]
        public async Task<ActionResult<Cliente>> ObtenerClientePorId(int id)
        {
            var cliente = await _clienteService.ObtenerClientePorIdAsync(id);
            if (cliente == null)
                return NotFound();

            return cliente;
        }

        [HttpPost("CrearCliente")]
        public async Task<ActionResult<Cliente>> CrearCliente([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _clienteService.CrearClienteAsync(cliente);
            if (!result)
                return StatusCode(500);

            return CreatedAtAction(nameof(ObtenerClientePorId), new { id = cliente.IdCliente }, cliente);
        }

        [HttpPut("ActualizarCliente/{id}")]
        public async Task<ActionResult<Cliente>> ActualizarCliente(int id, [FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != cliente.IdCliente)
                return BadRequest();

            var result = await _clienteService.ActualizarClienteAsync(cliente);
            if (!result)
                return StatusCode(500);

            return NoContent();
        }

        [HttpPost("EliminarCliente/{id}")]
        public async Task<ActionResult> EliminarCliente(int id)
        {
            var result = await _clienteService.EliminarClienteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
