using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetoFinal.Application.Dto
{
    public class SaleUpdateDTO
    {
        public string BillingDate { get; set; }
        public ICollection<SaleItensDTO> SaleItens { get; set; }
    }
}
