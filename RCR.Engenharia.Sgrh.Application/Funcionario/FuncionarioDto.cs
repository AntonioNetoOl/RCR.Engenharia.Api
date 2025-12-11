using System;
using System.ComponentModel.DataAnnotations;

namespace RCR.Engenharia.Sgrh.Application.Funcionarios
{
    public class FuncionarioDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Nome { get; set; } = null!;

        public string? Cargo { get; set; }

        [Required]
        public string Rg { get; set; } = null!;

        [Required]
        public string Cpf { get; set; } = null!;

        [Required]
        public string Celular { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public DateTime DataNascimento { get; set; }

        public DateTime? DataAdmissao { get; set; }

        public decimal? Salario { get; set; }

        /// <summary>
        /// Foto em bytes.
        /// No cadastro/edição principal você pode ignorar esse campo e usar endpoints específicos de foto.
        /// É útil para consultas/detalhes.
        /// </summary>
        public byte[]? Foto { get; set; }

        public bool Ativo { get; set; }
    }
}
