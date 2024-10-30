using projetoFinal.Application.Dto;
using projetoFinal.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetoFinal.Application.Interfaces
{
    public interface IClientService
    {
        Task AddClientsAsync(ClientDto clientDto);
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<ClientDto> GetClientById(Guid id);
        Task<bool> UpdateClientAsync(ClientDto clientDto, Guid id);
        Task<bool> DeleteClientAsync(Guid id);

    }
}
