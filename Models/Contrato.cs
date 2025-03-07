using HotChocolate;

namespace ConsignadoGraphQL.Models
{
    public class Contrato
    {
        [GraphQLDescription("Identificador único do contrato")]
        public int Id { get; set; }

        [GraphQLDescription("Valor total do contrato")]
        public decimal ValorTotal { get; set; }

        [GraphQLDescription("Quantidade de parcelas do contrato")]
        public int Parcelas { get; set; }

        [GraphQLDescription("Taxa de juros aplicada ao contrato")]
        public decimal TaxaJuros { get; set; }

        [GraphQLDescription("Identificador do benefício associado")]
        public int BeneficioId { get; set; }

        [GraphQLDescription("Benefício associado ao contrato")]
        public Beneficio Beneficio { get; set; } = default!;
    }
}
