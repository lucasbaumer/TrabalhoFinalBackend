using AutoMapper;
using projetoFinal.Application.Dto;
using projetoFinal.Application.Interfaces;
using projetoFinal.Core.Entities;
using projetoFinal.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetoFinal.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IMapper mapper, IClientRepository clientRepository)
        {
            _mapper = mapper;
            _clientRepository = clientRepository;
        }

        public async Task AddClientsAsync(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            var clientExists = await _clientRepository.GetClientByCpf(client.Cpf);

            if(clientExists != null)
            {
                throw new Exception("Cpf já cadastrado!");

            }

            await _clientRepository.AddClientAsync(client);
            await _clientRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteClientAsync(Guid id)
        {
            var client = await _clientRepository.GetClientById(id);

            if (client == null)
            {
                return false;
            }

            var hasSales = await _clientRepository.ClientHasSale(client.Id);

            if (hasSales)
            {
                return true;
            }

            _clientRepository.DeleteClient(client);
            await _clientRepository.SaveChangesAsync();
            return false;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            var clients = await _clientRepository.GetAllClientsAsync();
            return clients.ToList();
        }

        public async Task<ClientDto> GetClientById(Guid id)
        {
            var client = await _clientRepository.GetClientById(id);

            if(client == null)
            {
                return null;
            }

            return _mapper.Map<ClientDto>(client);
        }

        public async Task<bool> UpdateClientAsync(ClientDto clientDto, Guid id)
        {
            var client = await _clientRepository.GetClientById(id);

            if(client == null)
            {
                return false;
            }

            _mapper.Map(clientDto, client);
            _clientRepository.UpdateClient(client);
            await _clientRepository.SaveChangesAsync();
            return true;
        }
    }
}
