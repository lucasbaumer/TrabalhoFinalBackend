using projetoFinal.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetoFinal.Application.Dto
{
    public class SalesDTO
    {
        [Required(ErrorMessage = "A data da venda é obrigatória.")]
        public string SaleDate { get; set; }

        [Required(ErrorMessage = "A data de faturamento é obrigatória.")]
        public string BillingDate { get; set; }

        [Required(ErrorMessage = "O ID do cliente é obrigatório.")]
        public Guid ClientId { get; set; }

        public string ClientName { get; set; }

        [Required(ErrorMessage = "Os itens da venda são obrigatórios.")]
        public ICollection<SaleItensDTO> SaleItens { get; set; }
    }
}
