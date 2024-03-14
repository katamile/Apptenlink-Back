using Apptenlink_Back.Entities;
using Apptenlink_Back.Middleware.Exceptions.BadRequest;
using Apptenlink_Back.Repositories.ProductoRepository;
using Apptenlink_Back.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apptenlink_Back.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository ?? throw new ArgumentNullException(nameof(productoRepository));
        }

        public async Task<IEnumerable<Producto>> ListarTodosProductosAsync()
        {
            return await _productoRepository.ListarTodosAsync();
        }

        public async Task<Producto> ObtenerProductoPorIdAsync(int idProducto)
        {
            return await _productoRepository.ObtenerPorIdAsync(idProducto);
        }

        public async Task<Producto> ObtenerProductoPorIdentificacionAsync(string codigo)
        {
            if (string.IsNullOrEmpty(codigo))
                throw new InvalidFieldException("El código de producto no puede estar vacío.");

            return await _productoRepository.ObtenerPorCodigoAsync(codigo);
        }

        public async Task<bool> CrearProductoAsync(Producto producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));

            // Verificar si ya existe un producto con el mismo código
            var productoExistente = await _productoRepository.ObtenerPorCodigoAsync(producto.Codigo);
            if (productoExistente != null)
                throw new ExistingObjectException("Ya existe un producto con el mismo código.");

            producto.FechaCreacion = DateTime.Now;
            producto.Estado = Globales.ESTADO_ACTIVO;

            return await _productoRepository.CrearAsync(producto);
        }

        public async Task<bool> ActualizarProductoAsync(Producto producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));

            return await _productoRepository.ActualizarAsync(producto);
        }

        public async Task<bool> EliminarProductoAsync(int idProducto)
        {
            return await _productoRepository.EliminarAsync(idProducto);
        }
    }
}
