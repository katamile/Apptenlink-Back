using Apptelink_Back.Entities;

namespace Apptelink_Back.Repositories.UsuarioRepository
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ObtenerPorUsernameAsync(string username);
        Task<string> ValidarCredencialesAsync(string username, string contraseña);
        Task<bool> CambiarContraseñaAsync(string username, string nuevaContraseña);
    }
}