using System;

namespace RCR.Engenharia.Sgrh.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string? Cpf { get; private set; }
        public string? Login { get; private set; }
        public string SenhaHash { get; private set; }

        // Chave Estrangeira para o Perfil
        public Guid PerfilId { get; private set; }
        public virtual Perfil Perfil { get; private set; }

        private Usuario() { }

        public Usuario(string nome, string email, string senhaHash, Guid perfilId, string? cpf = null, string? login = null )
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;
            PerfilId = perfilId;
            Cpf = cpf;
            Login = login;
        }
    }
}