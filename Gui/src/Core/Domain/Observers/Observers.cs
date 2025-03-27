using Gui.Core.SharedKernel;

namespace Gui.Core.Domain.Observer;

public class Observer : BaseEntity
{
    public Guid Id { get; protected set; }
    public string Name { get; private set; }
    public Guid UserId { get; private set; }
    
    private readonly HashSet<Guid> _unitIds = new();
    public IReadOnlyCollection<Guid> UnitIds => _unitIds;

    public DateTimeOffset CreatedAt { get; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public DateTimeOffset? DeletedAt { get; protected set; }

    private Observer(string name, Guid userId)
    {
        Id = Guid.NewGuid();
        Name = name;
        UserId = userId;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public static Observer Create(string name, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(name));
        }

        if (userId == Guid.Empty)
        {
            throw new ArgumentException("UserId cannot be empty", nameof(userId));
        }

        return new Observer(name, userId);
    }

    public void AddUnit(Guid unitId)
    {
        if (!_unitIds.Contains(unitId))
        {
            _unitIds.Add(unitId);
        }
    }

    public void RemoveUnit(Guid unitId)
    {
        if (_unitIds.Contains(unitId))
        {
            _unitIds.Remove(unitId);
        }
    }
}