namespace Gui.Core.Domain.Units;

public interface IUnitRepository
{
    Task AddAsync(Unit unit, CancellationToken cancellationToken = default);
    Task<Unit?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<Unit?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}