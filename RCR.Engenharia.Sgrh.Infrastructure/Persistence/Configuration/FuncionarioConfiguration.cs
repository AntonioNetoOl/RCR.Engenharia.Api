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

            builder.Property(f => f.Rg)
                .HasColumnName("rg")
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(f => f.Cpf)
                .HasColumnName("cpf")
                .IsRequired()
                .HasMaxLength(14);

            builder.Property(f => f.Celular)
                .HasColumnName("celular")
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(f => f.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(f => f.DataNascimento)
                .HasColumnName("data_nascimento")
                .IsRequired()
                .HasColumnType("date");

            builder.Property(f => f.DataAdmissao)
                .HasColumnName("data_admissao")
                .HasColumnType("date");

            builder.Property(f => f.Salario)
                .HasColumnName("salario")
                .HasColumnType("decimal(18,2)");

            builder.Property(f => f.Foto)
                .HasColumnName("foto")
                .HasColumnType("varbinary(max)");

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
