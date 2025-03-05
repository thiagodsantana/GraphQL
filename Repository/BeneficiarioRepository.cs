using ConsignadoGraphQL.Models;

namespace ConsignadoGraphQL.Repository
{
    public static class BeneficiarioRepository
    {
        #region Gerando dados Fake
        private static int _beneficiarioId = 1;
        private static int _beneficioId = 1;
        private static int _contratoId = 1;

        public static List<Beneficiario> Beneficiarios { get; } = new()
    {
        new() { Id = _beneficiarioId++, Nome = "João Silva", CPF = "12345678901", Beneficios =
            [
                new() { Id = _beneficioId++, Tipo = "Aposentadoria", Valor = 3000m, BeneficiarioId = _beneficiarioId, Contratos =
                    [
                        new() { Id = _contratoId++, ValorTotal = 15000m, Parcelas = 24, TaxaJuros = 2.5m, BeneficioId = _beneficioId }
                    ]
                }
            ]
        },
        new() { Id = _beneficiarioId++, Nome = "Maria Oliveira", CPF = "98765432100", Beneficios =
            [
                new() { Id = _beneficioId++, Tipo = "Pensão", Valor = 2500m, BeneficiarioId = _beneficiarioId, Contratos =
                    [
                        new() { Id = _contratoId++, ValorTotal = 12000m, Parcelas = 36, TaxaJuros = 3.0m, BeneficioId = _beneficioId }
                    ]
                }
            ]
        }
    };
        #endregion

        #region Métodos de Consulta
        public static IQueryable<Beneficiario> GetBeneficiarios() => Beneficiarios.AsQueryable();

        public static Beneficiario? GetBeneficiarioById(int id)
            => Beneficiarios.FirstOrDefault(b => b.Id == id);

        public static Beneficiario? GetBeneficiarioByCPF(string cpf)
            => Beneficiarios.FirstOrDefault(b => b.CPF == cpf);
        #endregion

        #region Métodos de Inserção
        public static Beneficiario AddBeneficiario(Beneficiario novoBeneficiario)
        {
            novoBeneficiario.Id = _beneficiarioId++;
            foreach (var beneficio in novoBeneficiario.Beneficios)
            {
                beneficio.Id = _beneficioId++;
                beneficio.BeneficiarioId = novoBeneficiario.Id;
                foreach (var contrato in beneficio.Contratos)
                {
                    contrato.Id = _contratoId++;
                    contrato.BeneficioId = beneficio.Id;
                }
            }
            Beneficiarios.Add(novoBeneficiario);
            return novoBeneficiario;
        }
        #endregion
    }
}
