using Apptelink_Back.Entities;
using Apptelink_Back.Repositories.FacturaRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apptelink_Back.Services.FacturaService
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _facturaRepository;

        public FacturaService(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        public async Task<IEnumerable<Factura>> ListarTodasFacturas()
        {
            return await _facturaRepository.ListarTodosAsync();
        }

        public async Task<Factura> ObtenerFacturaPorId(int idFactura)
        {
            return await _facturaRepository.ObtenerPorIdAsync(idFactura);
        }

        public async Task<Factura> ObtenerFacturaPorNumero(string numeroFactura)
        {
            return await _facturaRepository.ObtenerPorNumeroAsync(numeroFactura);
        }

        public async Task<bool> CrearFactura(Factura factura)
        {
            try
            {
                return await _facturaRepository.CrearAsync(factura);
            }
            catch (Exception ex)
            {
                // Manejar el error aquí o lanzar una excepción para ser manejada en un nivel superior
                throw new Exception("Error al crear factura", ex);
            }
        }

        public async Task<bool> EliminarFactura(int idFactura)
        {
            try
            {
                return await _facturaRepository.EliminarAsync(idFactura);
            }
            catch (Exception ex)
            {
                // Manejar el error aquí o lanzar una excepción para ser manejada en un nivel superior
                throw new Exception("Error al eliminar factura", ex);
            }
        }
    }
}
