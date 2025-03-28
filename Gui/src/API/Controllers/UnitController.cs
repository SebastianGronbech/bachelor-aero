using Gui.Core.Domain.Units.Pipelines;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gui.API.Controllers;

[ApiController]
[Route("api/units")]
public class UnitController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ILogger<UnitController> _logger;

    public UnitController(ISender mediator, ILogger<UnitController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    public async Task<IActionResult> GetUnits(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all units");
        var units = await _mediator.Send(new Get.Request(), cancellationToken);

        return Ok(units);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUnit([FromBody] Create.Command request, CancellationToken cancellationToken)
    {
        // _logger.LogInformation("Creating unit with ID: {UnitId}", request.UnitId);
        var result = await _mediator.Send(request, cancellationToken);

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        return Created();
    }

    [HttpPost("{unitId}/operators")]
    public async Task<IActionResult> AddOperator(Guid unitId, [FromBody] AddOperator.Command request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding operator with user ID: {UserID} to unit with ID: {UnitId}", request.UserId, unitId);
        var command = new AddOperator.Command(unitId, request.UserId);
        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        return Created();
    }
}