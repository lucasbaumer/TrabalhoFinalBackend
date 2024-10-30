using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetoFinal.Core.Entities
{
    public class Sales
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string SaleDate { get; set; }

        [Required]
        public string BillingDate { get; set; }
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public Client Client { get; set; }

        public ICollection<SaleIten> SaleItens { get; set; }
    }
}
