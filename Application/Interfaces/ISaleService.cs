using projetoFinal.Application.Dto;
using projetoFinal.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetoFinal.Application.Interfaces
{
    public interface ISaleService
    {
        Task<IEnumerable<Sales>> GetAllSalesAsync();
        Task<SalesDTO> GetSaleById(Guid id);
        Task AddSaleAsync (SalesDTO salesDto);
        Task DeleteSaleAsync (Guid id);
        Task UpdateSaleAsync (SaleUpdateDTO salesUpdateDto, Guid id);

    }
}
