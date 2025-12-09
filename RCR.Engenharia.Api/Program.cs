using RCR.Engenharia.Sgrh.Application.Funcionarios;
using RCR.Engenharia.Sgrh.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// >>> REGISTROS DA CAMADA DE INFRA E APPLICATION <<<

// Infra: DbContext, Repositórios, UnitOfWork (usa a connection string)
builder.Services.AddInfrastructure(builder.Configuration);

// Application: service de funcionário
builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
