using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace projetoFinal.Application.Dto
{
    public class SaleItensDTO
    {
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [MaxLength(80, ErrorMessage = "A descrição não pode ter mais de 80 caracteres.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "O preço unitário é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço unitário deve ser maior que zero.")]
        public decimal UnityPrice { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1.")]
        public int Quantity { get; set; }
    }
}
