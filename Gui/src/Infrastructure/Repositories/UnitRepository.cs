using Gui.Core.Domain.Units;
using Gui.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Gui.Infrastructure.Repositories
{
    public class UnitRepository : IUnitRepository
    {
        private readonly ApplicationContext _dbContext;

        public UnitRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Unit unit, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(unit, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Unit?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Units
                .FirstOrDefaultAsync(u => u.Name == name, cancellationToken);
        }

        public async Task<Unit?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Units
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }
    }
}