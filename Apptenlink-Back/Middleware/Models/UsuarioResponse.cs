namespace Apptenlink_Back.Middleware.Models
{
    public class UsuarioLoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class CambiarContraseñaRequest
    {
        public string Username { get; set; }
        public string NuevaContraseña { get; set; }
    }

}
