using ConsignadoGraphQL.Models;

namespace ConsignadoGraphQL.GraphQL
{
    public class Subscription
    {
        // Subscrição para notificar quando um novo beneficiário for adicionado
        [Subscribe]
        [Topic]
        [GraphQLDescription("Notifica quando um novo beneficiário for adicionado.")]
        public Beneficiario OnBeneficiarioAdded([EventMessage] Beneficiario beneficiario) => beneficiario;
    }
}
