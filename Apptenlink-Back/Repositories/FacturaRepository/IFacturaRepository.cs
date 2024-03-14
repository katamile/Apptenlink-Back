using Apptenlink_Back.Entities;
using Apptenlink_Back.Repositories.GenericRepository;

namespace Apptenlink_Back.Repositories.VentaRepository
{
    public interface IFacturaRepository : IGenericRepository<Factura>
    {
        Task<Factura> Registrar(Factura entidad);
        Task<List<Factura>> Listar();
    }
}
