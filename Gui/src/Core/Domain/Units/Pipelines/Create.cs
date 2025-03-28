using FluentResults;
using Gui.Core.SharedKernel;
using MediatR;

namespace Gui.Core.Domain.Units.Pipelines;

public class Create
{
    public record Command(string Name) : IRequest<Result>;

    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitRepository unitRepository, IUnitOfWork unitOfWork)
        {
            _unitRepository = unitRepository ?? throw new ArgumentNullException(nameof(unitRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var existingUnit = await _unitRepository.GetByNameAsync(request.Name, cancellationToken);
            if (existingUnit != null)
            {
                return Result.Fail("Unit with the same name already exists");
            }

            var unit = Unit.Create(Guid.NewGuid(), request.Name);
            // await _unitRepository.AddAsync(unit, cancellationToken);

            _unitRepository.Add(unit);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}