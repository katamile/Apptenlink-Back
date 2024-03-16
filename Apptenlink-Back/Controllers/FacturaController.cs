using Apptelink_Back.Entities;
using Apptelink_Back.Services.FacturaService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apptelink_Back.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaService _facturaService;

        public FacturaController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        [HttpGet("ConsultarTodas")]
        public async Task<ActionResult<IEnumerable<Factura>>> ObtenerTodasFacturas()
        {
            var facturas = await _facturaService.ListarTodasFacturas();
            return Ok(facturas);
        }

        [HttpGet("ConsultarPorId/{idFactura}")]
        public async Task<ActionResult<Factura>> ObtenerFacturaPorId(int idFactura)
        {
            var factura = await _facturaService.ObtenerFacturaPorId(idFactura);
            if (factura == null)
            {
                return NotFound();
            }
            return Ok(factura);
        }

        [HttpGet("ConsultarFactura/{numeroFactura}")]
        public async Task<ActionResult<Factura>> ObtenerFacturaPorNumero(string numeroFactura)
        {
            var factura = await _facturaService.ObtenerFacturaPorNumero(numeroFactura);
            if (factura == null)
            {
                return NotFound();
            }
            return Ok(factura);
        }

        [HttpPost("CrearFactura")]
        public async Task<ActionResult> CrearFactura([FromBody] Factura factura)
        {
            try
            {
                var resultado = await _facturaService.CrearFactura(factura);
                if (resultado)
                {
                    return Ok("Factura creada exitosamente");
                }
                else
                {
                    return BadRequest("Error al crear la factura");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("EliminarFactura/{idFactura}")]
        public async Task<ActionResult> EliminarFactura(int idFactura)
        {
            try
            {
                var resultado = await _facturaService.EliminarFactura(idFactura);
                if (resultado)
                {
                    return Ok("Factura eliminada exitosamente");
                }
                else
                {
                    return NotFound("Factura no encontrada");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}
