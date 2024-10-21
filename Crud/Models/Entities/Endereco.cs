using System.ComponentModel.DataAnnotations;

namespace Crud.Models.Entities
{
    public abstract class Endereco
    {
        public TipoEndereco Tipo { get; set; }

        [Flags]
        public enum TipoEndereco
        {
            Fiscal,
            Cobranca,
            Entrega
        }

        public Guid Id { get; set; }

        [RegularExpression(@"^\d{8}$", ErrorMessage = @"O CEP deve seguir esse formato ""12345678""")]
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public Guid ClienteId { get; set; }
    }
}
