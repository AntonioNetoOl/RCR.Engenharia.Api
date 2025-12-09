using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCR.Engenharia.Sgrh.Domain.Entities;

namespace RCR.Engenharia.Sgrh.Domain.Interfaces.Repositories
{
    public interface IFuncionarioRepository
    {
        Task<Funcionario?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Funcionario>> ListarAsync(CancellationToken cancellationToken = default);

        Task AdicionarAsync(Funcionario funcionario, CancellationToken cancellationToken = default);
        void Atualizar(Funcionario funcionario);
        void Remover(Funcionario funcionario); 
    }
}

