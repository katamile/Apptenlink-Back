﻿using Apptenlink_Back.Middleware.Models;
using Apptenlink_Back.Services.UsuarioService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Apptenlink_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(
            [FromBody] UsuarioLoginRequest request)
        {
            var token = await _usuarioService.ValidarCredencialesAsync(request.Username, request.Password);
            if (token == null)
                return Unauthorized("Nombre de usuario o contraseña incorrectos.");

            return Ok(new { token });
        }

        [HttpPost("cambiar-contraseña")]
        public async Task<ActionResult> CambiarContraseña(
            [FromBody] CambiarContraseñaRequest request)
        {
            var resultado = await _usuarioService.CambiarContraseñaAsync(request.Username, request.NuevaContraseña);
            if (!resultado)
                return BadRequest("No se pudo cambiar la contraseña.");

            return Ok("Contraseña cambiada exitosamente.");
        }

    }
}
