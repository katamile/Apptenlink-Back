using Apptelink_Back.Entities;
using Apptelink_Back.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Apptelink_Back.Repositories.UsuarioRepository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbContextApptelink _contexto;

        public UsuarioRepository(DbContextApptelink contexto)
        {
            _contexto = contexto;
        }

        public async Task<Usuario> ObtenerPorUsernameAsync(string username)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Username == username &&
                                                                u.Estado == Globales.ESTADO_ACTIVO);
        }

        public async Task<string> ValidarCredencialesAsync(string username, string contraseña)
        {
            var usuario = await ObtenerPorUsernameAsync(username);

            if (usuario == null)
                return null;

            // Verificar la contraseña
            if (BCrypt.Net.BCrypt.Verify(contraseña, usuario.Contraseña))
            {
                // Si las credenciales son válidas, restablecer el contador de intentos fallidos
                usuario.IntentosFallidos = 0;

                // Generar el token JWT
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("JsonWebApiTokenWithSwaggerAuthorizationAuthenticationAspNetCore");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.Name, usuario.Username),
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                await _contexto.SaveChangesAsync();
                return tokenString;
            }
            else
            {
                // Si las credenciales son inválidas, incrementar el contador de intentos fallidos
                if (usuario.IntentosFallidos == null)
                    usuario.IntentosFallidos = 1;
                else
                    usuario.IntentosFallidos++;

                // Bloquear el usuario si se exceden los intentos fallidos
                if (usuario.IntentosFallidos >= 3)
                {
                    usuario.Estado = Globales.ESTADO_BLOQUEADO;
                }

                await _contexto.SaveChangesAsync();
                return null;
            }
        }

        public async Task<bool> CambiarContraseñaAsync(string username, string nuevaContraseña)
        {
            var usuario = await ObtenerPorUsernameAsync(username);

            if (usuario == null)
                return false;

            // Actualizar la contraseña
            usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(nuevaContraseña);
            usuario.IntentosFallidos = 0; // Restablecer el contador de intentos fallidos
            await _contexto.SaveChangesAsync();
            return true;
        }
    }
}
