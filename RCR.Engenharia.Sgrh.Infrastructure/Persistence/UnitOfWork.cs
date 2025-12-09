// RCR.Engenharia.Sgrh.Infrastructure/Persistence/UnitOfWork.cs
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

        public Task CommitAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
