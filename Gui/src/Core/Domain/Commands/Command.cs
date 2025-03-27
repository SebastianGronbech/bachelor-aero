using Gui.Core.SharedKernel;

namespace Gui.Core.Domain.Commands;

public class Command : BaseEntity
{
    public Guid Id { get; protected set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Guid UnitId { get; private set; }
    public Guid OperatorId { get; private set; }
    public DateTimeOffset CreatedAt { get; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public DateTimeOffset? DeletedAt { get; protected set; }

    private Command(string name, string description, Guid unitId, Guid operatorId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        UnitId = unitId;
        OperatorId = operatorId;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public static Command Create(string name, string description, Guid unitId, Guid operatorId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description cannot be empty", nameof(description));
        }

        if (unitId == Guid.Empty)
        {
            throw new ArgumentException("UnitId cannot be empty", nameof(unitId));
        }

        if (operatorId == Guid.Empty)
        {
            throw new ArgumentException("OperatorId cannot be empty", nameof(operatorId));
        }

        return new Command(name, description, unitId, operatorId);
    }
}