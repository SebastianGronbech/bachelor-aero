using FluentResults;
using MediatR;

namespace Gui.Core.Domain.Units.Pipelines;

public class AddOperator
{
    public record Command(Guid UnitId, Guid OperatorId) : IRequest<Result>;

    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly IUnitRepository _unitRepository;

        public Handler(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository ?? throw new ArgumentNullException(nameof(unitRepository));
        }

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var unit = await _unitRepository.GetByIdAsync(request.UnitId, cancellationToken);
            if (unit == null)
            {
                return Result.Fail("Unit not found");
            }

            unit.AddOperator(request.OperatorId);
            await _unitRepository.UpdateAsync(unit, cancellationToken);

            return Result.Ok();
        }
    }
}