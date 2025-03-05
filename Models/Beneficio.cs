namespace ConsignadoGraphQL.Models
{
    public class Beneficio
    {
        [GraphQLDescription("Identificador único do benefício")]
        public int Id { get; set; }

        [GraphQLDescription("Tipo do benefício (exemplo: Aposentadoria, Pensão)")]
        public required string Tipo { get; set; }

        [GraphQLDescription("Valor mensal do benefício")]
        public decimal Valor { get; set; }

        [GraphQLDescription("Identificador do beneficiário associado")]
        public int BeneficiarioId { get; set; }

        [GraphQLDescription("Beneficiário associado ao benefício")]
        public Beneficiario Beneficiario { get; set; } = default!;

        [GraphQLDescription("Lista de contratos vinculados ao benefício")]
        public List<Contrato> Contratos { get; set; } = [];
    }
}
