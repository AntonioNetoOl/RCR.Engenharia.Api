using Microsoft.AspNetCore.Identity;
using RCR.Engenharia.Sgrh.Domain.Entities;

namespace RCR.Engenharia.Sgrh.Infrastructure.Auth
{
    public class PasswordHashService
    {
        // PasswordHasher é a classe padrão do .NET para isso (seguro e testado)
        private readonly PasswordHasher<Usuario> _hasher = new PasswordHasher<Usuario>();

        // Gera o Hash a partir de uma senha texto puro
        public string HashSenha(Usuario user, string senhaPura)
        {
            return _hasher.HashPassword(user, senhaPura);
        }

        // Verifica se a senha informada bate com o Hash do banco
        public bool VerificaSenha(Usuario user, string senhaHashNoBanco, string senhaInformada)
        {
            var resultado = _hasher.VerifyHashedPassword(user, senhaHashNoBanco, senhaInformada);

            // Sucesso se for Success ou SuccessRehashNeeded (quando o algoritmo atualiza)
            return resultado != PasswordVerificationResult.Failed;
        }
    }
}