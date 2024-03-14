using Apptenlink_Back.Entities;
using Apptenlink_Back.Services.FacturaService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apptenlink_Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaService _facturaService;

        public FacturaController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarFactura([FromBody] Factura factura)
        {
            try
            {
                var facturaRegistrada = await _facturaService.RegistrarFactura(factura);
                return Ok(facturaRegistrada);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar registrar la factura: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListarFacturas()
        {
            try
            {
                var facturas = await _facturaService.ListarFacturas();
                return Ok(facturas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar listar las facturas: {ex.Message}");
            }
        }

    }
}
