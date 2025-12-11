using RCR.Engenharia.Sgrh.Domain.Interfaces;
using RCR.Engenharia.Sgrh.Infrastructure.Persistence.Context;

namespace RCR.Engenharia.Sgrh.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SgrhDbContext _context;

        public UnitOfWork(SgrhDbContext context)
        {
            _context = context;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
