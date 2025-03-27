using Gui.Core.SharedKernel;

namespace Gui.Core.Domain.Units;

public class Unit : BaseEntity
{
    public Guid Id { get; protected set; }
    public string Name { get; private set; }

    private readonly HashSet<Guid> _operatorIds = new();
    public IReadOnlyCollection<Guid> OperatorIds => _operatorIds;

    private readonly HashSet<Guid> _observerIds = new();
    public IReadOnlyCollection<Guid> ObserverIds => _observerIds;

    public DateTimeOffset CreatedAt { get; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public DateTimeOffset? DeletedAt { get; protected set; }

    private Unit(Guid id, string name)
    {
        Id = id;
        Name = name;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public static Unit Create(Guid id, string name)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty", nameof(id));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(name));
        }

        return new Unit(id, name);
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(name));
        }

        Name = name;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void AddOperator(Guid operatorId)
    {
        if (!_operatorIds.Contains(operatorId))
        {
            _operatorIds.Add(operatorId);
        }
    }

    public void RemoveOperator(Guid operatorId)
    {
        if (_operatorIds.Contains(operatorId))
        {
            _operatorIds.Remove(operatorId);
        }
    }

    public void AddObserver(Guid observerId)
    {
        if (!_observerIds.Contains(observerId))
        {
            _observerIds.Add(observerId);
        }
    }

    public void RemoveObserver(Guid observerId)
    {
        if (_observerIds.Contains(observerId))
        {
            _observerIds.Remove(observerId);
        }
    }
}
