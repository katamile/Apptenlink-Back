using Apptelink_Back.Entities;
using Apptelink_Back.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apptelink_Back.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService ?? throw new ArgumentNullException(nameof(productoService));
        }

        [HttpGet("ConsultarTodos")]
        public async Task<ActionResult<IEnumerable<Producto>>> ListarTodosProductos()
        {
            var productos = await _productoService.ListarTodosProductosAsync();
            return Ok(productos);
        }

        [HttpGet("ConsultarPorId/{id}")]
        public async Task<ActionResult<Producto>> ObtenerProductoPorId(int id)
        {
            var producto = await _productoService.ObtenerProductoPorIdAsync(id);
            if (producto == null)
                return NotFound();

            return producto;
        }

        [HttpPost("CrearProducto")]
        public async Task<ActionResult<Producto>> CrearProducto([FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productoService.CrearProductoAsync(producto);
            if (!result)
                return StatusCode(500); // Algo salió mal en el servidor

            return CreatedAtAction(nameof(ObtenerProductoPorId), new { id = producto.IdProducto }, producto);
        }

        [HttpPut("ActualizarProducto{id}")]
        public async Task<ActionResult<Producto>> ActualizarProducto(int id, [FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != producto.IdProducto)
                return BadRequest();

            var result = await _productoService.ActualizarProductoAsync(producto);
            if (!result)
                return StatusCode(500); // Algo salió mal en el servidor

            return NoContent();
        }

        [HttpPost("EliminarProducto/{id}")]
        public async Task<ActionResult> EliminarProducto(int id)
        {
            var result = await _productoService.EliminarProductoAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
