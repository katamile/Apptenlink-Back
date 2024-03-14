using Apptelink_Back.Entities;
using Apptelink_Back.Utils;
using Microsoft.EntityFrameworkCore;

namespace Apptelink_Back.Repositories.FacturaRepository
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly DbContextApptelink _contexto;

        public FacturaRepository(DbContextApptelink contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Factura>> ListarTodosAsync()
        {
            return await _contexto.Facturas.Include(f => f.DetalleFacturas).Where(f => f.Estado == Globales.ESTADO_ACTIVO).ToListAsync();
        }

        public async Task<Factura> ObtenerPorIdAsync(int idFactura)
        {
            return await _contexto.Facturas.Include(f => f.DetalleFacturas).FirstOrDefaultAsync(f => f.IdFactura == idFactura && f.Estado == Globales.ESTADO_ACTIVO);
        }

        public async Task<Factura> ObtenerPorNumeroAsync(string numeroFactura)
        {
            return await _contexto.Facturas.Include(f => f.DetalleFacturas).FirstOrDefaultAsync(
                                                    f => EF.Functions.Like(f.NumeroFactura, $"%{numeroFactura}%")
                                                    && f.Estado == Globales.ESTADO_ACTIVO);
        }


        public async Task<bool> CrearAsync(Factura factura)
        {
            factura.NumeroFactura = GenerarNumeroFactura();
            factura.Subtotal = CalcularSubtotal(factura);
            factura.PorcentajeIgv = ObtenerPorcentajeIGV(); // Debes definir cómo obtener este valor
            factura.Igv = factura.Subtotal * factura.PorcentajeIgv;
            factura.Total = factura.Subtotal + factura.Igv;

            factura.Estado = Globales.ESTADO_ACTIVO;
            factura.FechaCreacion = DateTime.Now;

            _contexto.Facturas.Add(factura);

            foreach (var detalle in factura.DetalleFacturas)
            {
                detalle.Subtotal = detalle.Precio * detalle.Cantidad;
                _contexto.Entry(detalle).State = EntityState.Added;

                // Disminuir la cantidad del stock del producto
                var producto = await _contexto.Productos.FindAsync(detalle.IdProducto);
                producto.Stock -= detalle.Cantidad;
                _contexto.Entry(producto).State = EntityState.Modified;
            }

            await _contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(int idFactura)
        {
            var factura = await _contexto.Facturas.FindAsync(idFactura);
            if (factura != null)
            {
                factura.Estado = Globales.ESTADO_INACTIVO;
                await _contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        private string GenerarNumeroFactura()
        {
            // Obtener la fecha actual
            DateTime fechaActual = DateTime.Today;

            // Obtener el número de facturas emitidas hoy
            int numeroFacturasHoy = _contexto.Facturas
                                               .Count(f => f.FechaCreacion.Date == fechaActual);

            // Incrementar en 1 para obtener el siguiente número de factura
            numeroFacturasHoy++;

            // Formatear el número de factura con el formato deseado (por ejemplo, FACT-YYYYMMDD-0001)
            string numeroFactura = $"FACT-{fechaActual:yyyyMMdd}-{numeroFacturasHoy:D4}";

            return numeroFactura;
        }


        private decimal CalcularSubtotal(Factura factura)
        {
            decimal subtotal = 0;
            foreach (var detalle in factura.DetalleFacturas)
            {
                subtotal += detalle.Precio * detalle.Cantidad;
            }
            return subtotal;
        }

        private decimal ObtenerPorcentajeIGV()
        {
            return 0.12m;
        }
    }
}
