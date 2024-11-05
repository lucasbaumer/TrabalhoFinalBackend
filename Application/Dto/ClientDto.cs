using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetoFinal.Application.Dto
{
    public class ClientDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [MaxLength(15, ErrorMessage = "O CPF deve ter no máximo 15 caracteres.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O email deve ter no máximo 50 caracteres.")]
        [EmailAddress(ErrorMessage = "O email não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [MaxLength(16, ErrorMessage = "O telefone deve ter no máximo 16 caracteres.")]
        public string Telefone { get; set; }

    }
}
