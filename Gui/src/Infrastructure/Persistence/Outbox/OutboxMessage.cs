namespace Gui.Infrastructure.Persistence.Outbox;
// ShardKernel

public sealed class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ProcessedAt { get; set; }
}