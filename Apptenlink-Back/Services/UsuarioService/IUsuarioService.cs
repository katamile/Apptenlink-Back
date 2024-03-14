using System.Threading.Tasks;

namespace Apptenlink_Back.Services.UsuarioService
{
    public interface IUsuarioService
    {
        Task<string> ValidarCredencialesAsync(string username, string contraseña);
        Task<bool> CambiarContraseñaAsync(string username, string nuevaContraseña);
    }
}
