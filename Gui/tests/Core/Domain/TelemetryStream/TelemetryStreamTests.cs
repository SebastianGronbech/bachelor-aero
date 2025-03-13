namespace Core.Domain.TelemetryStream;

public class TelemetryTests
{
    [Fact]
    public void AddNewPoint_AddsOnePoint()
    {
        // Arrange
        var telemetry = new TelemetryStream(Guid.NewGuid());

        var parameterName = "Temperature";
        var value = 25.0;
        var timestamp = DateTimeOffset.UtcNow;

        // Act
        telemetry.AddPoint(parameterName, value, timestamp);

        // Assert
        Assert.Single(telemetry.Points);
        // telemetry.Points.Should().HaveCount(1);
        // telemetry.Points.Count().ShouldBe(1);
    }

    [Fact]
    public void AddNewPointTwice_AddsTwoPoints()
    {
        // Arrange
        var telemetry = new TelemetryStream(Guid.NewGuid());

        var parameterName = "Temperature";
        var value = 25.0;
        var timestamp = DateTimeOffset.UtcNow;

        // Act
        telemetry.AddPoint(parameterName, value, timestamp);
        telemetry.AddPoint(parameterName, value, timestamp);

        // Assert
        Assert.Equal(2, telemetry.Points.Count);
    }
}