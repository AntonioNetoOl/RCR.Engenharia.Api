using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCR.Engenharia.Sgrh.Domain.Enums;

namespace RCR.Engenharia.Sgrh.Application.Funcionarios
{
    public class FuncionarioDto
    {
        public Guid Id { get; set; }       
        public string Nome { get; set; } = null!;
        public string? Cargo { get; set; }
        public DateTime? DataNascimento { get; set; }
        public bool Ativo { get; set; }
    }
}



