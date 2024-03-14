using Apptelink_Back.Entities;
using Apptelink_Back.Repositories.ProductoRepository;
using Apptelink_Back.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apptelink_Back.Repositories.ProductoRepository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly DbContextApptelink _contexto;

        public ProductoRepository(DbContextApptelink contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Producto>> ListarTodosAsync()
        {
            return await _contexto.Productos.Where(p => p.Estado == Globales.ESTADO_ACTIVO).ToListAsync();
        }

        public async Task<Producto> ObtenerPorIdAsync(int idProducto)
        {
            return await _contexto.Productos
                                   .Where(p => p.IdProducto == idProducto && p.Estado == Globales.ESTADO_ACTIVO)
                                   .FirstOrDefaultAsync();
        }

        public async Task<Producto> ObtenerPorCodigoAsync(string codigo)
        {
            return await _contexto.Productos
                                   .FirstOrDefaultAsync(p => p.Codigo == codigo && p.Estado == Globales.ESTADO_ACTIVO);
        }

        public async Task<bool> CrearAsync(Producto producto)
        {
            producto.FechaCreacion = DateTime.Now;
            producto.Estado = Globales.ESTADO_ACTIVO;
            _contexto.Productos.Add(producto);
            await _contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarAsync(Producto producto)
        {
            _contexto.Entry(producto).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(int idProducto)
        {
            var producto = await _contexto.Productos.FindAsync(idProducto);
            if (producto != null)
            {
                producto.Estado = Globales.ESTADO_INACTIVO;
                await _contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
