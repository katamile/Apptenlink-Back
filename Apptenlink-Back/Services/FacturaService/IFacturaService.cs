using Apptenlink_Back.Entities;

namespace Apptenlink_Back.Services.FacturaService
{
    public interface IFacturaService
    {
        Task<Factura> RegistrarFactura(Factura factura);
        Task<List<Factura>> ListarFacturas();
    }
}
