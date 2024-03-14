using Apptelink_Back.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apptelink_Back.Services.FacturaService
{
    public interface IFacturaService
    {
        Task<IEnumerable<Factura>> ListarTodasFacturas();
        Task<Factura> ObtenerFacturaPorId(int idFactura);
        Task<Factura> ObtenerFacturaPorNumero(string numeroFactura);
        Task<bool> CrearFactura(Factura factura);
        Task<bool> EliminarFactura(int idFactura);
    }
}
