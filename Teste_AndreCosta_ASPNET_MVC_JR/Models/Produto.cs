using System.ComponentModel.DataAnnotations;

namespace Teste_AndreCosta_ASPNET_MVC_JR.Models
{
    public class Produto
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(50), MinLength(1)]
        public string Nome { get; set; }

        public bool Ativo { get; set; }
    }
}
