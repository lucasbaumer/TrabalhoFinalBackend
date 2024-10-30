using projetoFinal.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetoFinal.Application.Dto
{
    public class SalesDTO
    {
        public string SaleDate { get; set; }
        public string BillingDate { get; set; }
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public ICollection<SaleItensDTO> SaleItens { get; set; }
    }
}
