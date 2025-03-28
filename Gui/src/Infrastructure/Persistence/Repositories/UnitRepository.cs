using Gui.Core.Domain.Units;
using Microsoft.EntityFrameworkCore;

namespace Gui.Infrastructure.Persistence.Repositories
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

        public void Add(Unit unit)
        {
            _dbContext.Add(unit);
        }

        public async Task<Unit?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Units
                .Include(u => u.Operators)
                .Include(u => u.Observers)
                .FirstOrDefaultAsync(u => u.Name == name, cancellationToken);
        }

        public async Task<Unit?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Units
                .Include(u => u.Operators)
                .Include(u => u.Observers)
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<List<Unit>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Units
                .Include(u => u.Operators)
                .Include(u => u.Observers)
                .ToListAsync(cancellationToken);
        }
    }
}