using Microsoft.EntityFrameworkCore;
using ConsignadoGraphQL.Models;

namespace ConsignadoGraphQL
{
    public class ConsignadoContext(DbContextOptions<ConsignadoContext> options) : DbContext(options)
    {
        public DbSet<Beneficiario> Beneficiarios { get; set; }
        public DbSet<Beneficio> Beneficios { get; set; }
        public DbSet<Contrato> Contratos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beneficiario>()
                        .ToTable("Beneficiarios")
                        .HasMany(b => b.Beneficios)
                        .WithOne(b => b.Beneficiario)
                        .HasForeignKey(b => b.BeneficiarioId);


            modelBuilder.Entity<Beneficio>()
                        .ToTable("Beneficios")
                        .HasMany(b => b.Contratos)
                        .WithOne(c => c.Beneficio)
                        .HasForeignKey(c => c.BeneficioId);


            modelBuilder.Entity<Contrato>()
                        .ToTable("Contratos");

                           
            // Seed Data
            modelBuilder.Entity<Beneficiario>().HasData(
                new Beneficiario
                {
                    Id = 1,
                    Nome = "João Silva",
                    CPF = "12345678901",
                    Beneficios = []
                },
                new Beneficiario
                {
                    Id = 2,
                    Nome = "Maria Oliveira",
                    CPF = "10987654321",
                    Beneficios = []
                }
            );

            modelBuilder.Entity<Beneficio>().HasData(
                new Beneficio
                {
                    Id = 1,
                    Tipo = "Aposentadoria",
                    Valor = 1200.00m,
                    BeneficiarioId = 1,
                    Contratos = []
                },
                new Beneficio
                {
                    Id = 2,
                    Tipo = "Pensão",
                    Valor = 800.00m,
                    BeneficiarioId = 2,
                    Contratos = []
                }
            );

            modelBuilder.Entity<Contrato>().HasData(
                new Contrato
                {
                    Id = 1,
                    ValorTotal = 24000.00m,
                    Parcelas = 24,
                    TaxaJuros = 1.5m,
                    BeneficioId = 1
                },
                new Contrato
                {
                    Id = 2,
                    ValorTotal = 9600.00m,
                    Parcelas = 12,
                    TaxaJuros = 2.0m,
                    BeneficioId = 2
                }
            );
        }
    }
}
