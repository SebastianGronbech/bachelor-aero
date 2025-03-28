using FluentResults;
using Gui.Core.SharedKernel;
using MediatR;

namespace Gui.Core.Domain.Units.Pipelines;

public class AddOperator
{
    public sealed record Command(Guid UnitId, Guid UserId) : IRequest<Result>;

    internal sealed class Handler : IRequestHandler<Command, Result>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IOperatorRepository _operatorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitRepository unitRepository, IOperatorRepository operatorRepository, IUnitOfWork unitOfWork)
        {
            _unitRepository = unitRepository ?? throw new ArgumentNullException(nameof(unitRepository));
            _operatorRepository = operatorRepository ?? throw new ArgumentNullException(nameof(operatorRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var unit = await _unitRepository.GetByIdAsync(request.UnitId, cancellationToken);
            if (unit == null)
            {
                return Result.Fail("Unit not found");
            }

            var @operator = unit.AddOperator(request.UserId);
            _operatorRepository.Add(@operator);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}