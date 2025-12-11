using System;

namespace RCR.Engenharia.Sgrh.Domain.Entities
{
    public class Funcionario
    {
        public Guid Id { get; private set; }

        public string Nome { get; private set; } = null!;
        public string? Cargo { get; private set; }

        public string Rg { get; private set; } = null!;
        public string Cpf { get; private set; } = null!;
        public string Celular { get; private set; } = null!;
        public string Email { get; private set; } = null!;

        public DateTime DataNascimento { get; private set; }
        public DateTime? DataAdmissao { get; private set; }
        public decimal? Salario { get; private set; }

        /// <summary>
        /// Foto em bytes (varbinary(max) no banco).
        /// Pode ser nula caso o funcionário ainda não tenha foto cadastrada.
        /// </summary>
        public byte[]? Foto { get; private set; }

        public bool Ativo { get; private set; }
        public DateTime CriadoEm { get; private set; }
        public DateTime? AtualizadoEm { get; private set; }

        // EF Core
        private Funcionario() { }

        public Funcionario(
            string nome,
            string rg,
            string cpf,
            string celular,
            string email,
            DateTime dataNascimento,
            string? cargo,
            DateTime? dataAdmissao,
            decimal? salario,
            bool ativo)
        {
            Id = Guid.NewGuid();
            CriadoEm = DateTime.UtcNow;
            AtualizadoEm = null;

            DefinirDadosBasicos(
                nome,
                rg,
                cpf,
                celular,
                email,
                dataNascimento,
                cargo,
                dataAdmissao,
                salario,
                ativo,
                alterarDataAtualizacao: false);
        }

        /// <summary>
        /// Atualiza todos os dados básicos do funcionário (exceto foto).
        /// </summary>
        public void Atualizar(
            string nome,
            string rg,
            string cpf,
            string celular,
            string email,
            DateTime dataNascimento,
            string? cargo,
            DateTime? dataAdmissao,
            decimal? salario,
            bool ativo)
        {
            DefinirDadosBasicos(
                nome,
                rg,
                cpf,
                celular,
                email,
                dataNascimento,
                cargo,
                dataAdmissao,
                salario,
                ativo,
                alterarDataAtualizacao: true);
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

        /// <summary>
        /// Define ou substitui a foto do funcionário.
        /// </summary>
        public void DefinirFoto(byte[] foto)
        {
            if (foto is null || foto.Length == 0)
                throw new ArgumentException("A foto do funcionário não pode ser vazia.", nameof(foto));

            Foto = foto;
            AtualizadoEm = DateTime.UtcNow;
        }

        /// <summary>
        /// Remove a foto do funcionário (define como null).
        /// </summary>
        public void RemoverFoto()
        {
            Foto = null;
            AtualizadoEm = DateTime.UtcNow;
        }

        #region Métodos privados de validação e atribuição

        private void DefinirDadosBasicos(
            string nome,
            string rg,
            string cpf,
            string celular,
            string email,
            DateTime dataNascimento,
            string? cargo,
            DateTime? dataAdmissao,
            decimal? salario,
            bool ativo,
            bool alterarDataAtualizacao)
        {
            ValidarTextoObrigatorio(nome, nameof(Nome), "Nome do funcionário é obrigatório.");
            ValidarTextoObrigatorio(rg, nameof(Rg), "RG do funcionário é obrigatório.");
            ValidarTextoObrigatorio(cpf, nameof(Cpf), "CPF do funcionário é obrigatório.");
            ValidarTextoObrigatorio(celular, nameof(Celular), "Celular do funcionário é obrigatório.");
            ValidarTextoObrigatorio(email, nameof(Email), "E-mail do funcionário é obrigatório.");

            if (dataNascimento == default)
                throw new ArgumentException("Data de nascimento do funcionário é obrigatória.", nameof(dataNascimento));

            Nome = nome.Trim();
            Rg = rg.Trim();
            Cpf = cpf.Trim();
            Celular = celular.Trim();
            Email = email.Trim();

            Cargo = string.IsNullOrWhiteSpace(cargo) ? null : cargo.Trim();
            DataNascimento = dataNascimento;
            DataAdmissao = dataAdmissao;
            Salario = salario;
            Ativo = ativo;

            if (alterarDataAtualizacao)
                AtualizadoEm = DateTime.UtcNow;
        }

        private static void ValidarTextoObrigatorio(string valor, string nomePropriedade, string mensagemErro)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException(mensagemErro, nomePropriedade);
        }

        #endregion
    }
}
