using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// 1. DTOs (Isso mantemos na Application, pois já criamos lá)
using RCR.Engenharia.Sgrh.Application.DTOs.Auth;

// 2. Entidades (Domain)
using RCR.Engenharia.Sgrh.Domain.Entities;

// 3. ONDE ESTÁ O TOKEN SERVICE (Voltamos para onde você criou: na API)
using RCR.Engenharia.Api.Services;
using RCR.Engenharia.Sgrh.Infrastructure.Auth; // <--- PasswordHashService está aqui// 4. Contexto do Banco (Infra)
using RCR.Engenharia.Sgrh.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Identity;

namespace RCR.Engenharia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Autenticacao")]
    public class AuthController : ControllerBase
    {
        private readonly SgrhDbContext _context;
        private readonly TokenService _tokenService;
        private readonly PasswordHashService _passwordHasher;

        public AuthController(SgrhDbContext context, TokenService tokenService, PasswordHashService passwordHasher)
        {
            _context = context;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Validação Básica
            if (string.IsNullOrEmpty(request.Login) || string.IsNullOrEmpty(request.Senha))
            {
                return BadRequest("Usuário/CPF e senha são obrigatórios.");
            }

            // Query incluindo Perfil e Permissões
            IQueryable<Usuario> query = _context.Usuarios
                .Include(u => u.Perfil)
                .ThenInclude(p => p.Permissoes);

            // Lógica de busca: CPF ou Login/Email
            if (request.TipoLogin?.ToLower() == "cpf")
            {
                query = query.Where(u => u.Cpf == request.Login);
            }
            else
            {
                query = query.Where(u => u.Login == request.Login || u.Email == request.Login);
            }

            var usuario = await query.FirstOrDefaultAsync();

            // Validações
            if (usuario == null) return Unauthorized("Usuário/CPF ou senha inválidos.");
            if (!_passwordHasher.VerificaSenha(usuario, usuario.SenhaHash, request.Senha))
            {
                return Unauthorized("Usuário/CPF ou senha inválidos.");
            }

            // Gera Token
            var tokenJwt = _tokenService.GenerateToken(usuario);

            // Resposta usando o DTO correto
            var resposta = new LoginResponse(
                Token: tokenJwt,
                NomeUsuario: usuario.Nome,
                Email: usuario.Email,
                Perfil: usuario.Perfil.Nome,
                Permissoes: usuario.Perfil.Permissoes.Select(p => p.Slug).ToList()
            );

            return Ok(resposta);
        }
    }
}