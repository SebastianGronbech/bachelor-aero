using MediatR;

namespace Gui.Core.Domain.Units.Pipelines;

public class IssueCommand
{
    public sealed record Request(Guid UnitId, Guid UserId) : IRequest<Unit>;

    public class Handler : IRequestHandler<Request, Unit>
    {
        private readonly IUnitRepository _unitRepository;

        public Handler(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository ?? throw new ArgumentNullException(nameof(unitRepository));
        }

        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            var unit = await _unitRepository.GetByIdAsync(request.UnitId, cancellationToken);
            if (unit == null)
            {
                throw new UnitNotFoundException(request.UnitId);
            }

            var @operator = unit.Operators.SingleOrDefault(o => o.UserId == request.UserId);
            if (@operator == null)
            {
                throw new OperatorNotFoundException(request.UserId);
            }

            if (unit.Status != UnitStatus.Ready)
            {
                throw new UnitNotReadyException(unit.Id);
            }

            unit.Issue(operator.UserId);
            _unitRepository.Update(unit);

            return unit;
        }
    }
}