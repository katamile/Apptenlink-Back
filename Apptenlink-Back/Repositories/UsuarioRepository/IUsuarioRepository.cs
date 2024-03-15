using Apptelink_Back.Entities;
using Apptenlink_Back.Middleware.Models;

namespace Apptelink_Back.Repositories.UsuarioRepository
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ObtenerPorUsernameAsync(string username);
        Task<string> ValidarCredencialesAsync(UsuarioLoginRequest request);
        Task<bool> CambiarContraseñaAsync(CambiarContraseñaRequest request);
    }
}