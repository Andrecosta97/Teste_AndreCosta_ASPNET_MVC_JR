using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Teste_AndreCosta_ASPNET_MVC_JR.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do cliente.")]
        [MaxLength(50), MinLength(1)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o e-mail do cliente")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage ="Digite um e-mail válido.")]
        public string Email { get; set; }

        
        [Required(ErrorMessage = "Informe o CPF do cliente.")]
        [MinLength(14, ErrorMessage ="Digite um CPF válido!")]
        public string CPF { get; set; }

        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "Um produto precisa ser relacionado ao cliente.")]
        public string Produto { get; set; }

        public List<string> Produtos { get; set; } = new List<string>();        

        
    }
}
