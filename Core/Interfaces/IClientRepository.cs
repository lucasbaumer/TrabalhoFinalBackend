using projetoFinal.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetoFinal.Core.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<bool> ClientHasSale(Guid clientId);
        Task<Client> GetClientById(Guid id);
        Task AddClientAsync(Client client);
        void UpdateClient(Client client);
        void DeleteClient(Client client);
        Task<Client> GetClientByCpf(string cpf);
        Task<bool> SaveChangesAsync();
    }
}
