using MediatR;

namespace Gui.Core.Domain.Units.Pipelines;

public class Get
{
    public sealed record Request : IRequest<List<Unit>> { }

    public class Handler : IRequestHandler<Request, List<Unit>>
    {
        private readonly IUnitRepository _unitRepository;

        public Handler(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public async Task<List<Unit>> Handle(Request request, CancellationToken cancellationToken)
        {
            return await _unitRepository.GetAllAsync(cancellationToken);
        }
    }
}