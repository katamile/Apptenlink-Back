using Apptenlink_Back.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apptenlink_Back.Repositories.ProductoRepository
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> ListarTodosAsync();
        Task<Producto> ObtenerPorIdAsync(int idProducto);
        Task<Producto> ObtenerPorCodigoAsync(string codigo);
        Task<bool> CrearAsync(Producto producto);
        Task<bool> ActualizarAsync(Producto producto);
        Task<bool> EliminarAsync(int idProducto);
    }
}
