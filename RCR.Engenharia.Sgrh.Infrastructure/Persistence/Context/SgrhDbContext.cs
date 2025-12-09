using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using RCR.Engenharia.Sgrh.Domain.Entities;

namespace RCR.Engenharia.Sgrh.Infrastructure.Persistence.Context
{
    public class SgrhDbContext : DbContext
    {
        public SgrhDbContext(DbContextOptions<SgrhDbContext> options)
            : base(options)
        {
        }

        public DbSet<Funcionario> Funcionarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SgrhDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
