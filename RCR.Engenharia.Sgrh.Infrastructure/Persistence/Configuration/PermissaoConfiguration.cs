using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCR.Engenharia.Sgrh.Domain.Entities;

namespace RCR.Engenharia.Sgrh.Infrastructure.Persistence.Configuration
{
    public class PermissaoConfiguration : IEntityTypeConfiguration<Permissao>
    {
        public void Configure(EntityTypeBuilder<Permissao> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Slug).IsRequired().HasMaxLength(100);
            builder.HasIndex(x => x.Slug).IsUnique(); // Garante que não existam dois slugs iguais
        }
    }
}