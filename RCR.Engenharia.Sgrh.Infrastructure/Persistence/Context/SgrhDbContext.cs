using Microsoft.EntityFrameworkCore;
using RCR.Engenharia.Sgrh.Domain.Entities; // <--- GARANTA QUE TEM ESSE USING

namespace RCR.Engenharia.Sgrh.Infrastructure.Persistence.Context
{
    public class SgrhDbContext : DbContext
    {
        public SgrhDbContext(DbContextOptions<SgrhDbContext> options) : base(options)
        {
        }

        // Tabela que já existia
        public DbSet<Funcionario> Funcionarios { get; set; }

        // --- ADICIONE ESTAS 3 LINHAS ---
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Permissao> Permissoes { get; set; }
        // -------------------------------

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica as configurações (Fluent API) que criamos nas pastas Configuration
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SgrhDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}