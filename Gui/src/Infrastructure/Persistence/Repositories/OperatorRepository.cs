using Gui.Core.Domain.Units;
using Microsoft.EntityFrameworkCore;

namespace Gui.Infrastructure.Persistence.Repositories;

public class OperatorRepository : IOperatorRepository
{
    private readonly ApplicationContext _dbContext;

    public OperatorRepository(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Operator @operator)
    {
        _dbContext.Operators.Add(@operator);
    }

    public async Task<Operator?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Operators
            .Where(o => o.UserId == userId)
            .SingleOrDefaultAsync(cancellationToken);
    }
}