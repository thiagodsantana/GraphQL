using ConsignadoGraphQL.Models;
using ConsignadoGraphQL.Repository;

namespace ConsignadoGraphQL.GraphQL
{
    public class Mutation
    {
        public Beneficiario AddBeneficiario(
            BeneficiarioInput input)
        {
            var beneficiario = new Beneficiario
            {
                Nome = input.Nome,
                CPF = input.CPF,
                Beneficios = input.Beneficios.Select(b => new Beneficio
                {
                    Tipo = b.Tipo,
                    Valor = b.Valor,
                    Contratos = b.Contratos.Select(c => new Contrato
                    {
                        ValorTotal = c.ValorTotal,
                        Parcelas = c.Parcelas,
                        TaxaJuros = c.TaxaJuros
                    }).ToList()
                }).ToList()
            };

            BeneficiarioRepository.AddBeneficiario(beneficiario);
            return beneficiario;
        }
    }

    public record BeneficiarioInput(string Nome, string CPF, List<BeneficioInput> Beneficios);
    public record BeneficioInput(string Tipo, decimal Valor, List<ContratoInput> Contratos);
    public record ContratoInput(decimal ValorTotal, int Parcelas, decimal TaxaJuros);

}
