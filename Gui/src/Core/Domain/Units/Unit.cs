using Gui.Core.SharedKernel;

namespace Gui.Core.Domain.Units;

public sealed class Unit : BaseEntity
{
    public Guid Id { get; private init; }
    public string Name { get; private set; }

    private readonly HashSet<Operator> _operators = new();
    public IReadOnlyCollection<Operator> Operators => _operators;

    private readonly HashSet<Observer> _observers = new();
    public IReadOnlyCollection<Observer> Observers => _observers;

    private readonly HashSet<TelemetryStream> _telemetryStreams = new();
    public IReadOnlyCollection<TelemetryStream> TelemetryStreams => _telemetryStreams;

    public DateTimeOffset CreatedAt { get; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }

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

    public Operator AddOperator(Guid userId)
    {
        if (_operators.Any(o => o.UserId == userId))
        {
            throw new InvalidOperationException("User is already an operator");
        }

        var @operator = new Operator(Id, userId);
        _operators.Add(@operator);

        UpdatedAt = DateTimeOffset.UtcNow;

        return @operator;
    }

    public void RemoveOperator(Guid userId)
    {
        var operatorToRemove = _operators.FirstOrDefault(o => o.UserId == userId);
        if (operatorToRemove == null)
        {
            throw new InvalidOperationException("User is not an operator");
        }

        _operators.Remove(operatorToRemove);
    }

    public Observer AddObserver(Guid userId)
    {
        if (_observers.Any(o => o.UserId == userId))
        {
            throw new InvalidOperationException("User is already an observer");
        }

        var observer = new Observer(Id, userId);
        _observers.Add(observer);

        UpdatedAt = DateTimeOffset.UtcNow;

        return observer;
    }

    // public void RemoveObserver(Guid observerId)
    // {
    //     if (_observers.Contains(observerId))
    //     {
    //         _observers.Remove(observerId);
    //     }
    // }

    public TelemetryStream AddTelemetryStream(Guid id, string name, string description)
    {
        if (_telemetryStreams.Any(ts => ts.Id == id))
        {
            throw new InvalidOperationException("Telemetry stream with this ID already exists");
        }

        var telemetryStream = TelemetryStream.Create(id, Id, name, description);
        _telemetryStreams.Add(telemetryStream);

        UpdatedAt = DateTimeOffset.UtcNow;

        return telemetryStream;
    }
}
