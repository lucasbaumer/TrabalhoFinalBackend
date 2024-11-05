using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace projetoFinal.Core.Entities
{
    public class SaleIten
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(80)]
        public string description { get; set; }
        [Required]
        public decimal UnityPrice { get; set; }
        [Required]
        public int Quantity { get; set; } = 1;

        [JsonIgnore]
        public Guid SaleId { get; set; }
        public Sales Sale { get; set; }

    }
}
