using Apptenlink_Back.Entities;
using Apptenlink_Back.Repositories.VentaRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apptenlink_Back.Services.FacturaService
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _facturaRepository;

        public FacturaService(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        public async Task<Factura> RegistrarFactura(Factura factura)
        {
            if (factura == null)
            {
                throw new ArgumentNullException(nameof(factura), "La factura no puede ser nula.");
            }

            if (string.IsNullOrEmpty(factura.NumeroFactura))
            {
                throw new ArgumentException("El número de factura no puede estar vacío.", nameof(factura.NumeroFactura));
            }

            // Puedes agregar más validaciones según tus necesidades

            try
            {
                // Registrar la factura
                return await _facturaRepository.Registrar(factura);
            }
            catch (Exception ex)
            {
                // Manejar cualquier error y relanzar la excepción
                throw new Exception("Error al intentar registrar la factura.", ex);
            }
        }


        public async Task<List<Factura>> ListarFacturas()
        {
            try
            {
                // Obtener la lista de facturas
                return await _facturaRepository.Listar();
            }
            catch (Exception ex)
            {
                // Manejar cualquier error y relanzar la excepción
                throw new Exception("Error al intentar listar las facturas.", ex);
            }
        }

    }
}
