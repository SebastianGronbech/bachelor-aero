using Core.SharedKernel;

namespace Core.Domain.TelemetryStream;

public class TelemetryStream : BaseEntity
{
    public Guid Id { get; protected set; }
    // public string Name { get; private set; }

    // public Guid TenantId { get; protected set; }

    private readonly List<DataPoint> _points = new();
    public IReadOnlyCollection<DataPoint> Points => _points.AsReadOnly();

    public DateTimeOffset LastUpdated { get; protected set; }

    public TelemetryStream(
        Guid id
        // Guid tenantId, 
        // string name
        )
    {
        Id = id;
        // TenantId = tenantId;
        // Name = name;
        LastUpdated = DateTimeOffset.UtcNow;
    }

    public void AddPoint(string parameterName, double value, DateTimeOffset timestamp)
    {
        if (string.IsNullOrWhiteSpace(parameterName))
        {
            throw new ArgumentException("Parameter name cannot be null or empty", nameof(parameterName));
        }

        if (timestamp > DateTimeOffset.UtcNow)
        {
            throw new ArgumentException("Timestamp cannot be in the future", nameof(timestamp));
        }

        var point = new DataPoint(parameterName, value, timestamp);
        _points.Add(point);
        LastUpdated = DateTimeOffset.UtcNow;
    }
}