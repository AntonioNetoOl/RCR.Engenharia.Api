using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RCR.Engenharia.Sgrh.Application.Funcionarios
{
    public interface IFuncionarioService
    {
        Task<FuncionarioDto?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<FuncionarioDto>> ListarAsync(CancellationToken cancellationToken = default);
        Task<Guid> CriarAsync(FuncionarioDto dto, CancellationToken cancellationToken = default);
        Task AtualizarAsync(Guid id, FuncionarioDto dto, CancellationToken cancellationToken = default);
        Task RemoverAsync(Guid id, CancellationToken cancellationToken = default);
        Task AtualizarFotoAsync(Guid id, byte[] foto, CancellationToken cancellationToken = default);
        Task RemoverFotoAsync(Guid id, CancellationToken cancellationToken = default);
    }
}

