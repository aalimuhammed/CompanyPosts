using CompanyPost.API.Model;
using CompanyPost.Application.CQRS.Commands.InComing;

namespace CompanyPost.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IncomingController : ControllerBase
{ 
	private readonly IMediator _mediator;
	public IncomingController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet("GetIncomingMaxSerialNumber")]
	public async Task<IActionResult> GetMaxSerialNumberAsync(CancellationToken cancellationToken)
	{
		var query = new GetInComingMaxSerialNumberQuery();
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}

    [HttpGet("GetDocumentNumbers")]
    public async Task<IActionResult> GetInComingDocumentNumbers(CancellationToken cancellationToken)
    {
        var query = new GetInComingDocumentsNumbersQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("GetInComingToBeCopied/{id}")]
    public async Task<IActionResult> GetInComingToBeCopied(Guid Id, CancellationToken cancellationToken)
    {
        var query = new GetInComingToBeCopiedQuery(Id);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost("CreateIncoming")]
	public async Task<IActionResult> CreateIncomingAsync(
		[FromForm] CreateIncomingDTO createIncomingDTO,
		CancellationToken cancellationToken)
	{
		try
		{
			var command = new CreateIncomingCommand(createIncomingDTO);
			await _mediator.Send(command, cancellationToken);
			return Ok(new ApiResponse { Success = true, Message = "Data has been saved successfully ✅" });
		}
		catch (Exception ex)
		{
			return BadRequest(new ApiResponse { Success = false, Message = $"An error occurred while saving the data: {ex.Message}" });
		}
	}
}