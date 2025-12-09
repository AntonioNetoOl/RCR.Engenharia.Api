using Microsoft.EntityFrameworkCore;
using RCR.Engenharia.Sgrh.Domain.Entities;
using RCR.Engenharia.Sgrh.Domain.Interfaces.Repositories;
using RCR.Engenharia.Sgrh.Infrastructure.Persistence.Context;

namespace RCR.Engenharia.Sgrh.Infrastructure.Persistence.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly SgrhDbContext _context;

        public FuncionarioRepository(SgrhDbContext context)
        {
            _context = context;
        }

        public async Task<Funcionario?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Funcionarios
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Funcionario>> ListarAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Funcionarios
                .AsNoTracking()
                .OrderBy(f => f.Nome)  
                .ToListAsync(cancellationToken);
        }


        public async Task AdicionarAsync(Funcionario funcionario, CancellationToken cancellationToken = default)
        {
            await _context.Funcionarios.AddAsync(funcionario, cancellationToken);
        }

        public void Atualizar(Funcionario funcionario)
        {
            _context.Funcionarios.Update(funcionario);
        }

        public void Remover(Funcionario funcionario)
        {
            _context.Funcionarios.Remove(funcionario);
        }
    }
}
