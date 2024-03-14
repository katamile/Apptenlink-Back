using Apptenlink_Back.Entities;
using Apptenlink_Back.Repositories.GenericRepository;
using Apptenlink_Back.Repositories.VentaRepository;
using Apptenlink_Back.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apptenlink_Back.Repositories.FacturaRepository
{
    public class FacturaRepository : GenericRepository<Factura>, IFacturaRepository
    {
        private readonly DbContextApptelink _dbContext;

        public FacturaRepository(DbContextApptelink dbContex) : base(dbContex)
        {
            _dbContext = dbContex;
        }

        public async Task<Factura> Registrar(Factura entidad)
        {
            Factura facturaGenerada = new Factura();

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {

                    // Validar que haya al menos un detalle en la factura
                    if (entidad.DetalleFacturas == null || !entidad.DetalleFacturas.Any())
                    {
                        throw new Exception("La factura debe tener al menos un detalle de ítem.");
                    }

                    // Calcular el subtotal sumando los subtotales de todos los detalles de la factura
                    decimal subtotal = entidad.DetalleFacturas.Sum(d => d.Subtotal ?? 0);

                    // Obtener el porcentaje de IGV (Impuesto General a las Ventas)
                    decimal porcentajeIGV = 0.13m;

                    // Calcular el IGV multiplicando el subtotal por el porcentaje de IGV
                    decimal igv = subtotal * porcentajeIGV;

                    // Calcular el total sumando el subtotal y el IGV
                    decimal total = subtotal + igv;

                    // Asignar los valores calculados a la entidad de factura
                    entidad.Subtotal = subtotal;
                    entidad.PorcentajeIgv = porcentajeIGV;
                    entidad.Igv = igv;
                    entidad.Total = total;

                    // Generar el número de factura utilizando la fecha y un valor autoincrementable
                    string fechaFactura = DateTime.Now.ToString("yyyyMMdd"); // Formato: AñoMesDía
                    int ultimoNumero = await ObtenerUltimoNumeroFactura(fechaFactura);
                    string numeroFactura = $"{fechaFactura}-{ultimoNumero + 1}";

                    // Verificar si el cliente existe en la base de datos
                    var cliente = await _dbContext.Clientes.FindAsync(entidad.IdCliente);
                    if (cliente == null)
                    {
                        throw new Exception("El cliente seleccionado no existe en la base de datos.");
                    }

                    // Guardar la factura en la base de datos
                    await _dbContext.Facturas.AddAsync(entidad);
                    await _dbContext.SaveChangesAsync();

                    // Actualizar el stock de los productos en el detalle de la factura
                    foreach (var detalle in entidad.DetalleFacturas)
                    {
                        var producto = await _dbContext.Productos.FindAsync(detalle.IdProducto);
                        if (producto == null)
                        {
                            throw new Exception($"El producto con ID {detalle.IdProducto} no existe en la base de datos.");
                        }

                        // Validar que haya suficiente stock disponible
                        if (producto.Stock < detalle.Cantidad)
                        {
                            throw new Exception($"No hay suficiente stock disponible para el producto con ID {detalle.IdProducto}.");
                        }

                        // Actualizar el stock del producto
                        if (producto.Stock >= detalle.Cantidad)
                        {
                            producto.Stock -= detalle.Cantidad;
                        }
                        else
                        {
                            throw new Exception($"No hay suficiente stock disponible para el producto con ID {detalle.IdProducto}.");
                        }
                        _dbContext.Productos.Update(producto);

                    }

                    // Guardar los cambios en el stock de productos
                    await _dbContext.SaveChangesAsync();

                    // Confirmar la transacción
                    transaction.Commit();

                    facturaGenerada = entidad;
                }
                catch (Exception ex)
                {
                    // Si ocurre algún error, realizar rollback de la transacción
                    transaction.Rollback();
                    throw ex;
                }
            }

            return facturaGenerada;
        }

        public async Task<List<Factura>> Listar()
        {
            // Obtener la lista de facturas incluyendo los detalles de ítems y la información del cliente
            var facturas = await _dbContext.Facturas
                .Include(f => f.DetalleFacturas)
                .Include(f => f.IdClienteNavigation)
                .ToListAsync();

            return facturas;
        }

        private async Task<int> ObtenerUltimoNumeroFactura(string fechaFactura)
        {
            var ultimaFactura = await _dbContext.Facturas
                .Where(f => f.NumeroFactura.StartsWith(fechaFactura))
                .OrderByDescending(f => f.IdFactura)
                .FirstOrDefaultAsync();

            if (ultimaFactura != null)
            {
                string[] partes = ultimaFactura.NumeroFactura.Split('-');
                if (partes.Length == 2 && int.TryParse(partes[1], out int ultimoNumero))
                {
                    return ultimoNumero;
                }
            }

            return 0; // Si no se encuentra ninguna factura para esa fecha, el último número es 0
        }
    }
}
