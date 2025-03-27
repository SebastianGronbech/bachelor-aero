using FluentResults;
using MediatR;

namespace Gui.Core.Domain.Units.Pipelines;

public class Create
{
    public record Command(string Name) : IRequest<Result>;

    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly IUnitRepository _unitRepository;

        public Handler(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository ?? throw new ArgumentNullException(nameof(unitRepository));
        }

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var existingUnit = await _unitRepository.GetByNameAsync(request.Name, cancellationToken);
            if (existingUnit != null)
            {
                return Result.Fail("Unit with the same name already exists");
            }

            var unit = Unit.Create(Guid.NewGuid(), request.Name);
            await _unitRepository.AddAsync(unit, cancellationToken);

            return Result.Ok();
        }
    }
}