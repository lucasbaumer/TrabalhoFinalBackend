using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace projetoFinal.Application.Dto
{
    public class SaleItensDTO
    {
        public string Description { get; set; }
        public decimal UnityPrice { get; set; }
        public int Quantity { get; set; }
    }
}
