using Apptenlink_Back.Entities;

namespace Apptenlink_Back.Repositories.ClienteRepositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ListarTodosAsync();
        Task<Cliente> ObtenerPorIdAsync(int idCliente);
        Task<bool> CrearAsync(Cliente cliente);
        Task<bool> ActualizarAsync(Cliente cliente);
        Task<bool> EliminarAsync(int idCliente);
    }
}
