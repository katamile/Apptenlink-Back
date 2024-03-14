using Apptelink_Back.Entities;

namespace Apptelink_Back.Repositories.FacturaRepository
{
    public interface IFacturaRepository
    {
        Task<IEnumerable<Factura>> ListarTodosAsync();
        Task<Factura> ObtenerPorIdAsync(int idFactura);
        Task<Factura> ObtenerPorNumeroAsync(string numeroFactura);
        Task<bool> CrearAsync(Factura factura);
        Task<bool> EliminarAsync(int idFactura);
    }
}
