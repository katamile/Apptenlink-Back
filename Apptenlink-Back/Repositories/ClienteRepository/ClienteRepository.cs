using Apptelink_Back.Entities;
using Apptelink_Back.Utils;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apptelink_Back.Repositories.ClienteRepository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DbContextApptelink _contexto;

        public ClienteRepository(DbContextApptelink contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Cliente>> ListarTodosAsync()
        {
            return await _contexto.Clientes.Where(c => c.Estado == Globales.ESTADO_ACTIVO).ToListAsync();
        }

        public async Task<Cliente> ObtenerPorIdAsync(int idCliente)
        {
            return await _contexto.Clientes
                                   .Where(c => c.IdCliente == idCliente && c.Estado == Globales.ESTADO_ACTIVO)
                                   .FirstOrDefaultAsync();
        }

        public async Task<Cliente> ObtenerPorIdentificacionAsync(string identificacion)
        {
            return await _contexto.Clientes
                                   .Where(c => c.Identificacion == identificacion && c.Estado == Globales.ESTADO_ACTIVO)
                                   .FirstOrDefaultAsync();
        }


        public async Task<bool> CrearAsync(Cliente cliente)
        {
            cliente.FechaCreacion = DateTime.Now;
            cliente.Estado = Globales.ESTADO_ACTIVO;
            _contexto.Clientes.Add(cliente);
            await _contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarAsync(Cliente cliente)
        {
            _contexto.Entry(cliente).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(int idCliente)
        {
            var cliente = await _contexto.Clientes.FindAsync(idCliente);
            if (cliente != null)
            {
                cliente.Estado = Globales.ESTADO_INACTIVO;
                await _contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
