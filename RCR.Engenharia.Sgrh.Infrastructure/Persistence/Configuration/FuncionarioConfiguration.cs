using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCR.Engenharia.Sgrh.Domain.Entities;

namespace RCR.Engenharia.Sgrh.Infrastructure.Persistence.Configuration
{
    public class FuncionarioConfiguration : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.ToTable("funcionarios", "dbo");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id)
                .HasColumnName("id")
                .ValueGeneratedNever(); // Guid gerado na aplicação

            builder.Property(f => f.Nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(f => f.Cargo)
                .HasColumnName("cargo")
                .HasMaxLength(120);

            builder.Property(f => f.DataNascimento)
                .HasColumnName("data_nascimento")
                .HasColumnType("date");

            builder.Property(f => f.Ativo)
                .HasColumnName("ativo")
                .IsRequired();

            builder.Property(f => f.CriadoEm)
                .HasColumnName("criado_em")
                .IsRequired();

            builder.Property(f => f.AtualizadoEm)
                .HasColumnName("atualizado_em");
        }
    }
}
