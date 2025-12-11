using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RCR.Engenharia.Api.Services;
using RCR.Engenharia.Sgrh.Application.Extensions;      // <--- Novo Using
using RCR.Engenharia.Sgrh.Infrastructure.Extensions;   // <--- Novo Using
using RCR.Engenharia.Sgrh.Infrastructure.Persistence;
using RCR.Engenharia.Sgrh.Infrastructure.Persistence.Context;
using System.Text;
using RCR.Engenharia.Sgrh.Infrastructure.Auth;

var builder = WebApplication.CreateBuilder(args);

// ==================================================================
// 1. REGISTRO DE SERVIÇOS (ORGANIZADO)
// ==================================================================

// A. Serviços Básicos da API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// B. Camadas da Arquitetura (Aqui está a mágica da limpeza!)
// O Program.cs não precisa mais saber QUAL service ou repository existe.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// C. Serviço de Token da API
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<PasswordHashService>();

// D. Configuração de Segurança (JWT)
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]!);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"]
    };
});

var app = builder.Build();

// ==================================================================
// 2. INICIALIZAÇÃO DO BANCO (SEED)
// ==================================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<SgrhDbContext>();
        await DbInitializer.InitializeAsync(context);

        // Dica: Use Console.WriteLine se o logger der muito trabalho no debug
        Console.WriteLine("✅ Banco de dados sincronizado!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Erro no banco: {ex.Message}");
    }
}

// ==================================================================
// 3. PIPELINE HTTP
// ==================================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication(); // 1º Quem é você?
app.UseAuthorization();  // 2º O que você pode fazer?

app.MapControllers();

app.Run();