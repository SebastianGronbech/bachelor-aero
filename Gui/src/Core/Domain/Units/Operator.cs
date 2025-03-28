namespace Gui.Core.Domain.Units;

public class Operator
{
    public Guid UnitId { get; private init; }
    public Guid UserId { get; private init; }

    public DateTimeOffset CreatedAt { get; }

    internal Operator(Guid unitId, Guid userId)
    {
        UnitId = unitId;
        UserId = userId;
        CreatedAt = DateTimeOffset.UtcNow;
    }
}