using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCR.Engenharia.Sgrh.Domain.Entities;

namespace RCR.Engenharia.Sgrh.Infrastructure.Persistence.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
            builder.HasIndex(x => x.Email).IsUnique(); // Email único no sistema

            // Um Usuário tem UM Perfil
            builder.HasOne(u => u.Perfil)
                   .WithMany(p => p.Usuarios)
                   .HasForeignKey(u => u.PerfilId)
                   .OnDelete(DeleteBehavior.Restrict); // Se apagar o perfil, não apaga o usuário (dá erro)
        }
    }
}