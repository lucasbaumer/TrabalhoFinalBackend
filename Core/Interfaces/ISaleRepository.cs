using projetoFinal.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetoFinal.Core.Interfaces
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sales>> GetAllSalesAsync();
        Task<Sales> GetSaleById(Guid id);
        Task AddSaleAsync(Sales sale);
        void UpdateSale(Sales sale);
        void DeleteSaleAsync(Sales sale);
        Task<bool> SaveChangesAsync();

    }
}
