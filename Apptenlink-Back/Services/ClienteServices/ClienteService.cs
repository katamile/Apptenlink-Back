using Apptenlink_Back.Entities;
using Apptenlink_Back.Middleware.Exceptions.BadRequest;
using Apptenlink_Back.Middleware.Exceptions.NotFound;
using Apptenlink_Back.Repositories.ClienteRepository;
using Apptenlink_Back.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apptenlink_Back.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository ?? throw new BadRequestException(nameof(clienteRepository));
        }

        public async Task<IEnumerable<Cliente>> ListarTodosClientesAsync()
        {
            return await _clienteRepository.ListarTodosAsync();
        }

        public async Task<Cliente> ObtenerClientePorIdAsync(int idCliente)
        {
            if (idCliente <= 0)
                throw new InvalidIdException();

            var cliente = await _clienteRepository.ObtenerPorIdAsync(idCliente);
            if (cliente == null)
                throw new NotFoundException("El cliente no fue encontrado.");

            return cliente;
        }

        public async Task<bool> CrearClienteAsync(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));

            // Verificar si ya existe un cliente con la misma identificación
            var clienteExistente = await _clienteRepository.ObtenerPorIdentificacionAsync(cliente.Identificacion);
            if (clienteExistente != null)
                throw new ExistingObjectException("Ya existe un cliente con la misma identificación.");

            cliente.FechaCreacion = DateTime.Now;
            cliente.Estado = Globales.ESTADO_ACTIVO;

            return await _clienteRepository.CrearAsync(cliente);
        }

        public async Task<bool> ActualizarClienteAsync(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));

            // Verificar si el cliente a actualizar existe
            var clienteExistente = await _clienteRepository.ObtenerPorIdAsync(cliente.IdCliente);
            if (clienteExistente == null)
                throw new NotFoundException("El cliente a actualizar no fue encontrado.");

            return await _clienteRepository.ActualizarAsync(cliente);
        }

        public async Task<bool> EliminarClienteAsync(int idCliente)
        {
            if (idCliente <= 0)
                throw new InvalidIdException();

            // Verificar si el cliente a eliminar existe
            var clienteExistente = await _clienteRepository.ObtenerPorIdAsync(idCliente);
            if (clienteExistente == null)
                throw new NotFoundException("El cliente a eliminar no fue encontrado.");

            return await _clienteRepository.EliminarAsync(idCliente);
        }
    }
}
