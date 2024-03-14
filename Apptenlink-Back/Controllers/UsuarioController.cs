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
        public async Task<ActionResult<string>> Login(string username, string contraseña)
        {
            var token = await _usuarioService.ValidarCredencialesAsync(username, contraseña);
            if (token == null)
                return Unauthorized("Nombre de usuario o contraseña incorrectos.");

            return Ok(new { token });
        }

        [HttpPost("cambiar-contraseña")]
        public async Task<ActionResult> CambiarContraseña(string username, string nuevaContraseña)
        {
            var resultado = await _usuarioService.CambiarContraseñaAsync(username, nuevaContraseña);
            if (!resultado)
                return BadRequest("No se pudo cambiar la contraseña.");

            return Ok("Contraseña cambiada exitosamente.");
        }
    }
}
