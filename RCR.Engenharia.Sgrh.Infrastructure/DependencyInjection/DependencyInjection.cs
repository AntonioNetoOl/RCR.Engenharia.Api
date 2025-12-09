using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RCR.Engenharia.Sgrh.Domain.Interfaces;
using RCR.Engenharia.Sgrh.Domain.Interfaces.Repositories;
using RCR.Engenharia.Sgrh.Infrastructure.Persistence;
using RCR.Engenharia.Sgrh.Infrastructure.Persistence.Context;
using RCR.Engenharia.Sgrh.Infrastructure.Persistence.Repositories;

namespace RCR.Engenharia.Sgrh.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<SgrhDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
