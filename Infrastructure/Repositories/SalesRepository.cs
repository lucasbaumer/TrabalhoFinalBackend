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
    public class SalesRepository : ISaleRepository
    {
        private readonly AppDbContext _context;
        public SalesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddSaleAsync(Sales sale)
        {
            await _context.Set<Sales>().AddAsync(sale);
        }

        public async void DeleteSaleAsync(Sales sale)
        {
             _context.Set<Sales>().Remove(sale);
        }

        public async Task<IEnumerable<Sales>> GetAllSalesAsync()
        {
           return await _context.Set<Sales>()
                          .Include(i => i.SaleItens)
                          .ToListAsync();
        }

        public async Task<Sales> GetSaleById(Guid id)
        {
            return await _context.Set<Sales>()
                                 .Include(i => i.SaleItens)
                                 .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateSale(Sales sale)
        {
            _context.Set<Sales>().Update(sale);
        }
    }
}
