using Microsoft.EntityFrameworkCore;
using projetoFinal.Core.Entities;
using projetoFinal.Core.Interfaces;
using projetoFinal.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetoFinal.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddClientAsync(Client client)
        {
            await _context.clients.AddAsync(client);
        }

        public async Task<bool> ClientHasSale(Guid clientId)
        {
            return await _context.sales.AnyAsync(x => x.ClientId == clientId);
        }

        public void DeleteClient(Client client)
        {
             _context.clients.Remove(client);
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
           return await _context.clients.ToListAsync();
        }

        public async Task<Client> GetClientByCpf(string cpf)
        {
            return await _context.clients.FirstOrDefaultAsync(x => x.Cpf == cpf);
        }

        public async Task<Client> GetClientById(Guid id)
        {
            return await _context.clients.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public void UpdateClient(Client client)
        {
            _context.clients.Update(client);
        }
    }
}
