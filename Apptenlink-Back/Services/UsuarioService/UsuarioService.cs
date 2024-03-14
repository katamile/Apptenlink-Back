using Apptelink_Back.Repositories.UsuarioRepository;
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
            return await _usuarioRepository.ValidarCredencialesAsync(username, contraseña);
        }

        public async Task<bool> CambiarContraseñaAsync(string username, string nuevaContraseña)
        {
            return await _usuarioRepository.CambiarContraseñaAsync(username, nuevaContraseña);
        }
    }
}
