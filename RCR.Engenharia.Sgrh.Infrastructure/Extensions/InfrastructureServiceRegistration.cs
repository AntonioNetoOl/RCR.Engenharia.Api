using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RCR.Engenharia.Sgrh.Domain.Interfaces;
using RCR.Engenharia.Sgrh.Domain.Interfaces.Repositories;
using RCR.Engenharia.Sgrh.Infrastructure.Persistence;
using RCR.Engenharia.Sgrh.Infrastructure.Persistence.Context;
using RCR.Engenharia.Sgrh.Infrastructure.Persistence.Repositories;

namespace RCR.Engenharia.Sgrh.Infrastructure.Extensions
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Banco de Dados
            services.AddDbContext<SgrhDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // 2. Repositórios
            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            // services.AddScoped<IPerfilRepository, PerfilRepository>();

            // 3. Unit of Work (A LINHA QUE FALTOU)
            // "Sempre que alguém pedir IUnitOfWork, entregue a classe UnitOfWork"
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}