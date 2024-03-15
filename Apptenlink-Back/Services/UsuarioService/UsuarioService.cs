using Apptelink_Back.Repositories.UsuarioRepository;
using Apptenlink_Back.Middleware.Models;
using System.Threading.Tasks;

namespace Apptenlink_Back.Services.UsuarioService
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<string> ValidarCredencialesAsync(string username, string contraseña)
        {
            var request = new UsuarioLoginRequest
            {
                Username = username,
                Password = contraseña
            };
            return await _usuarioRepository.ValidarCredencialesAsync(request);
        }

        public async Task<bool> CambiarContraseñaAsync(string username, string nuevaContraseña)
        {
            var request = new CambiarContraseñaRequest
            {
                Username = username,
                NuevaContraseña = nuevaContraseña
            };
            return await _usuarioRepository.CambiarContraseñaAsync(request);
        }



    }
}
