namespace Gui.Core.Domain.Units;

public class TelemetryStream
{
    public Guid Id { get; private init; }
    public Guid UnitId { get; private init; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    private readonly HashSet<DataPoint> _dataPoints = new();
    public IReadOnlyCollection<DataPoint> DataPoints => _dataPoints;

    public DateTimeOffset CreatedAt { get; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }

    internal TelemetryStream(Guid id, Guid unitId, string name, string description)
    {
        Id = id;
        UnitId = unitId;
        Name = name;
        Description = description;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public static TelemetryStream Create(Guid id, Guid unitId, string name, string description)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty", nameof(id));
        }

        if (unitId == Guid.Empty)
        {
            throw new ArgumentException("UnitId cannot be empty", nameof(unitId));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description cannot be empty", nameof(description));
        }

        return new TelemetryStream(id, unitId, name, description);
    }

    public void AddDataPoint(DateTimeOffset timestamp, double value)
    {
        if (timestamp == default)
        {
            throw new ArgumentException("Timestamp cannot be default", nameof(timestamp));
        }

        var dataPoint = DataPoint.Create(Id, timestamp, value);
        _dataPoints.Add(dataPoint);
    }
}