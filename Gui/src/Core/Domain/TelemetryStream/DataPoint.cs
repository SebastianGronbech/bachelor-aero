namespace Core.Domain.TelemetryStream;

public class DataPoint
{
    public string ParameterName { get; }
    public double Value { get; }
    public DateTimeOffset Timestamp { get; }

    public DataPoint(string parameterName, double value, DateTimeOffset timestamp)
    {
        if (string.IsNullOrWhiteSpace(parameterName))
        {
            throw new ArgumentException("Parameter name cannot be null or empty", nameof(parameterName));
        }

        ParameterName = parameterName;
        Value = value;
        Timestamp = timestamp;
    }
}