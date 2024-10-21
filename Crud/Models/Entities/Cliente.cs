using System.ComponentModel.DataAnnotations;

namespace Crud.Models.Entities
{
    public class Cliente
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        [RegularExpression(@"^\d{14}$", ErrorMessage = @"O CNPJ deve possuir 14 dígitos: ""00000000000000""")]
        public string Cnpj { get; set; }
        public List<Endereco> Enderecos { get; set; }
    }
}
