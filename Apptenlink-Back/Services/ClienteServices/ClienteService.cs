using Apptenlink_Back.Entities;
using Apptenlink_Back.Repositories.ClienteRepositories;
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
            _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));
        }

        public async Task<IEnumerable<Cliente>> ListarTodosClientesAsync()
        {
            return await _clienteRepository.ListarTodosAsync();
        }

        public async Task<Cliente> ObtenerClientePorIdAsync(int idCliente)
        {
            return await _clienteRepository.ObtenerPorIdAsync(idCliente);
        }

        public async Task<bool> CrearClienteAsync(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));

            cliente.FechaCreacion = DateTime.Now;
            cliente.Estado = Globales.ESTADO_ACTIVO;

            return await _clienteRepository.CrearAsync(cliente);
        }

        public async Task<bool> ActualizarClienteAsync(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));

            return await _clienteRepository.ActualizarAsync(cliente);
        }

        public async Task<bool> EliminarClienteAsync(int idCliente)
        {
            return await _clienteRepository.EliminarAsync(idCliente);
        }
    }
}
