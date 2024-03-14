using Apptelink_Back.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apptelink_Back.Services
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>> ListarTodosProductosAsync();
        Task<Producto> ObtenerProductoPorIdAsync(int idProducto);
        Task<bool> CrearProductoAsync(Producto producto);
        Task<bool> ActualizarProductoAsync(Producto producto);
        Task<bool> EliminarProductoAsync(int idProducto);
    }
}
