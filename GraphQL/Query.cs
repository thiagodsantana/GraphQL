using ConsignadoGraphQL.Models;
using ConsignadoGraphQL.Repository;

namespace ConsignadoGraphQL.GraphQL
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Lista de beneficiários com seus respectivos benefícios e contratos.")]
        public IQueryable<Beneficiario> GetBeneficiarios() => BeneficiarioRepository.GetBeneficiarios();
    }
}
