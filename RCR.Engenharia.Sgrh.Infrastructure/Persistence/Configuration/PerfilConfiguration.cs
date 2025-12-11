using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCR.Engenharia.Sgrh.Domain.Entities;

namespace RCR.Engenharia.Sgrh.Infrastructure.Persistence.Configuration
{
    public class PerfilConfiguration : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(100);

            // Configuração do Muitos-para-Muitos (Perfil <-> Permissao)
            // Cria uma tabela oculta chamada "PerfilPermissoes" para ligar os dois
            builder.HasMany(p => p.Permissoes)
                   .WithMany(p => p.Perfis)
                   .UsingEntity(j => j.ToTable("PerfilPermissoes"));
        }
    }
}