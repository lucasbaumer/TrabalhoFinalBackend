using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetoFinal.Application.Dto
{
    public class SaleUpdateDTO
    {
        public string BillingDate { get; set; }

        [Required(ErrorMessage = "Os itens da venda são obrigatórios.")]
        public ICollection<SaleItensDTO> SaleItens { get; set; }
    }
}
