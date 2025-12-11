using Microsoft.Extensions.DependencyInjection;
using RCR.Engenharia.Sgrh.Application.Funcionarios; // Importe seus services

namespace RCR.Engenharia.Sgrh.Application.Extensions
{
    public static class ApplicationServiceRegistration
    {
        // O "this" na frente transforma isso em um Método de Extensão
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Aqui você registra TODOS os seus Services
            services.AddScoped<IFuncionarioService, FuncionarioService>();

            // Futuramente:
            // services.AddScoped<IPontoService, PontoService>();
            // services.AddScoped<IDocumentoService, DocumentoService>();

            return services;
        }
    }
}