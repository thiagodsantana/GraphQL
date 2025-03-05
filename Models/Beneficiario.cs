namespace ConsignadoGraphQL.Models
{
    using System.Collections.Generic;

    public class Beneficiario
    {
        [GraphQLDescription("Identificador único do beneficiário")]
        public int Id { get; set; }

        [GraphQLDescription("Nome completo do beneficiário")]
        public required string Nome { get; set; }

        [GraphQLDescription("CPF do beneficiário")]
        public required string CPF { get; set; }

        [GraphQLDescription("Lista de benefícios do beneficiário")]
        public List<Beneficio> Beneficios { get; set; } = new();
    }
}
