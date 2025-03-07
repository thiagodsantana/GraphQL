using ConsignadoGraphQL.Models;
using ConsignadoGraphQL.Repository;

namespace ConsignadoGraphQL.GraphQL
{
    public class Query(BeneficiarioRepository beneficiarioRepository)
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Lista de beneficiários com seus respectivos benefícios e contratos.")]
        public IQueryable<Beneficiario> GetBeneficiarios() => beneficiarioRepository.GetBeneficiarios();
    }
}
