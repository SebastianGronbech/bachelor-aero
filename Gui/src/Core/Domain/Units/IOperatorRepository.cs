namespace Gui.Core.Domain.Units;

public interface IOperatorRepository
{
    void Add(Operator @operator);
    Task<Operator?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}