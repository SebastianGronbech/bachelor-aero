namespace Gui.Core.Domain.Units;

public class DataPoint
{
    public Guid TelemetryStreamId { get; private init; }
    public DateTimeOffset Timestamp { get; private init; }
    public double Value { get; private init; }

    internal DataPoint(Guid telemetryStreamId, DateTimeOffset timestamp, double value)
    {
        TelemetryStreamId = telemetryStreamId;
        Timestamp = timestamp;
        Value = value;
    }

    public static DataPoint Create(Guid telemetryStreamId, DateTimeOffset timestamp, double value)
    {
        if (telemetryStreamId == Guid.Empty)
        {
            throw new ArgumentException("TelemetryStreamId cannot be empty", nameof(telemetryStreamId));
        }

        return new DataPoint(telemetryStreamId, timestamp, value);
    }
}
