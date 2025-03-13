using MediatR;

namespace Core.Domain.Telemetry.Pipelines;

public class AddPoint
{
    public record Request(
        Guid TelemetryId,
        string Name,
        double Value,
        DateTime Timestamp
    ) : IRequest<Response>;

    public record Response(bool Success, string[] Errors);

    public class Handler : IRequestHandler<Request, Response>
    {
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}