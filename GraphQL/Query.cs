using ConsignadoGraphQL.Models;

namespace ConsignadoGraphQL.GraphQL
{
    public class Query
    {
        #region Gerando dados Fake
        private static int _beneficiarioId = 1;
        private static int _beneficioId = 1;
        private static int _contratoId = 1;

        public static List<Beneficiario> Beneficiarios { get; } =
    [
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
    },
    new() { Id = _beneficiarioId++, Nome = "Carlos Mendes", CPF = "65432198745", Beneficios =
        [
            new() { Id = _beneficioId++, Tipo = "Aposentadoria", Valor = 2800m, BeneficiarioId = _beneficiarioId, Contratos =
                [
                    new() { Id = _contratoId++, ValorTotal = 18000m, Parcelas = 30, TaxaJuros = 2.8m, BeneficioId = _beneficioId },
                    new() { Id = _contratoId++, ValorTotal = 10000m, Parcelas = 20, TaxaJuros = 3.2m, BeneficioId = _beneficioId }
                ]
            }
        ]
    },
    new() { Id = _beneficiarioId++, Nome = "Ana Souza", CPF = "11223344556", Beneficios =
        [
            new() { Id = _beneficioId++, Tipo = "Pensão", Valor = 3200m, BeneficiarioId = _beneficiarioId, Contratos =
                [
                    new() { Id = _contratoId++, ValorTotal = 25000m, Parcelas = 48, TaxaJuros = 2.0m, BeneficioId = _beneficioId }
                ]
            },
            new() { Id = _beneficioId++, Tipo = "Auxílio Doença", Valor = 1500m, BeneficiarioId = _beneficiarioId, Contratos =
                [
                    new() { Id = _contratoId++, ValorTotal = 8000m, Parcelas = 18, TaxaJuros = 3.5m, BeneficioId = _beneficioId }
                ]
            }
        ]
    },
    new() { Id = _beneficiarioId++, Nome = "Roberto Lima", CPF = "77889966554", Beneficios =
        [
            new() { Id = _beneficioId++, Tipo = "Aposentadoria", Valor = 4000m, BeneficiarioId = _beneficiarioId, Contratos =
                [
                    new() { Id = _contratoId++, ValorTotal = 20000m, Parcelas = 36, TaxaJuros = 2.2m, BeneficioId = _beneficioId },
                    new() { Id = _contratoId++, ValorTotal = 5000m, Parcelas = 12, TaxaJuros = 3.8m, BeneficioId = _beneficioId }
                ]
            }
        ]
    }
    ];
        #endregion

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Lista de beneficiários com seus respectivos benefícios e contratos.")]
        public IQueryable<Beneficiario> GetBeneficiarios() => Beneficiarios.AsQueryable();
    }
}
