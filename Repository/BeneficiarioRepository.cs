using ConsignadoGraphQL.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsignadoGraphQL.Repository
{
    public class BeneficiarioRepository(ConsignadoContext context)
    {
        public IQueryable<Beneficiario> GetBeneficiarios()
        {
            return context.Beneficiarios
                           .Include(b => b.Beneficios)
                           .ThenInclude(ben => ben.Contratos)
                            .AsQueryable();

        }

        public Beneficiario? GetBeneficiarioById(int id)
        {
            return context.Beneficiarios
                .Include(b => b.Beneficios)
                .FirstOrDefault(b => b.Id == id);
        }

        public Beneficiario AddBeneficiario(Beneficiario novoBeneficiario)
        {
            context.Beneficiarios.Add(novoBeneficiario);

            // Atribui IDs e relacionamentos antes de salvar no banco
            foreach (var beneficio in novoBeneficiario.Beneficios)
            {
                context.Beneficios.Add(beneficio);

                foreach (var contrato in beneficio.Contratos)
                {
                    context.Contratos.Add(contrato);
                }
            }

            context.SaveChanges();
            return novoBeneficiario;
        }
    }
}
