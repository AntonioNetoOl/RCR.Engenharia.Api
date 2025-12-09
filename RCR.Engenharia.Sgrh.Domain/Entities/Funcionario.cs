namespace RCR.Engenharia.Sgrh.Domain.Entities
{
    public class Funcionario
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; } = null!;
        public string? Cargo { get; private set; }
        public DateTime? DataNascimento { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime CriadoEm { get; private set; }
        public DateTime? AtualizadoEm { get; private set; }

        private Funcionario() { }

        public Funcionario(string nome, string? cargo, DateTime? dataNascimento, bool ativo)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Cargo = cargo;
            DataNascimento = dataNascimento;
            Ativo = ativo;
            CriadoEm = DateTime.UtcNow;
        }

        public void Atualizar(string nome, string? cargo, DateTime? dataNascimento, bool ativo, string nomeCompleto)
        {
            Nome = nome;
            Cargo = cargo;
            DataNascimento = dataNascimento;
            Ativo = ativo;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Ativar()
        {
            Ativo = true;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Desativar()
        {
            Ativo = false;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Atualizar(string nome, string? cargo, DateTime? dataNascimento, bool ativo)
        {
            throw new NotImplementedException();
        }
    }
}
