using Microsoft.IdentityModel.Tokens;
using RCR.Engenharia.Sgrh.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RCR.Engenharia.Api.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Usuario usuario)
        {
            // 1. Pega a chave do appsettings
            var keyString = _configuration["Jwt:Key"];
            var key = Encoding.ASCII.GetBytes(keyString!);

            // 2. Define as "Claims" (Dados que vão dentro do Token)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Perfil.Nome) // Ex: "Master"
            };

            // Adiciona cada permissão como uma Claim separada
            // Isso permite usar [Authorize(Policy="MENU.PONTO")] depois
            foreach (var permissao in usuario.Perfil.Permissoes)
            {
                claims.Add(new Claim("Permissao", permissao.Slug));
            }

            // 3. Configura o Token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8), // Duração do crachá (8 horas de trabalho)
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            // 4. Gera a string final
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}