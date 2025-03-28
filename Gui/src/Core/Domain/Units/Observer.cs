namespace Gui.Core.Domain.Units;

public class Observer
{
    public Guid UnitId { get; private init; }
    public Guid UserId { get; private init; }

    public DateTimeOffset CreatedAt { get; }

    internal Observer(Guid unitId, Guid userId)
    {
        UnitId = unitId;
        UserId = userId;
        CreatedAt = DateTimeOffset.UtcNow;
    }
}