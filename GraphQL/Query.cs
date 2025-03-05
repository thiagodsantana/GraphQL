using ConsignadoGraphQL.Models;

namespace ConsignadoGraphQL.GraphQL
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Lista de beneficiários com seus respectivos benefícios e contratos.")]
        public IQueryable<Beneficiario> GetBeneficiarios() => Repository.GetBeneficiarios();
    }
}
