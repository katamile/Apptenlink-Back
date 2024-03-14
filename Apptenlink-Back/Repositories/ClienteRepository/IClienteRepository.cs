using Apptelink_Back.Entities;

namespace Apptelink_Back.Repositories.ClienteRepository
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ListarTodosAsync();
        Task<Cliente> ObtenerPorIdAsync(int idCliente);
        Task<Cliente> ObtenerPorIdentificacionAsync(string identificacion);
        Task<bool> CrearAsync(Cliente cliente);
        Task<bool> ActualizarAsync(Cliente cliente);
        Task<bool> EliminarAsync(int idCliente);
    }
}
