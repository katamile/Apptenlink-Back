using Apptelink_Back.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apptelink_Back.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> ListarTodosClientesAsync();
        Task<Cliente> ObtenerClientePorIdAsync(int idCliente);
        Task<bool> CrearClienteAsync(Cliente cliente);
        Task<bool> ActualizarClienteAsync(Cliente cliente);
        Task<bool> EliminarClienteAsync(int idCliente);
    }
}
