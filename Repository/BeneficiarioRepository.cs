using ConsignadoGraphQL.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsignadoGraphQL.Repository
{
    public class BeneficiarioRepository
    {
        private readonly ConsignadoContext _context;

        public BeneficiarioRepository(ConsignadoContext context)
        {
            _context = context;
        }

        public IQueryable<Beneficiario> GetBeneficiarios()
            => _context.Beneficiarios
                        .Include(b => b.Beneficios)
                            .ThenInclude(ben => ben.Contratos)
                        .AsQueryable();

        
        #region Métodos de Inserção
        public Beneficiario AddBeneficiario(Beneficiario novoBeneficiario)
        {
            _context.Beneficiarios.Add(novoBeneficiario);

            // Atribui IDs e relacionamentos antes de salvar no banco
            foreach (var beneficio in novoBeneficiario.Beneficios)
            {
                _context.Beneficios.Add(beneficio);

                foreach (var contrato in beneficio.Contratos)
                {
                    _context.Contratos.Add(contrato);
                }
            }

            _context.SaveChanges();
            return novoBeneficiario;
        }
        #endregion
    }
}
