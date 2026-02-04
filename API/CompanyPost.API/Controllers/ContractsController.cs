using CompanyPost.API.Model;

namespace CompanyPost.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContractsController : ControllerBase
{
	private readonly IMediator _mediator;
	public ContractsController(IMediator mediator)
	{
		_mediator = mediator;
	}
	[HttpGet("GetContractMaxSerialNumber")]
	public async Task<IActionResult> GetMaxSerialNumberAsync(CancellationToken cancellationToken)
	{
		var query = new GetContractMaxSerialNumberQuery();
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}

	[HttpGet("GetContractRefMaxSerialNumber/{id}")]
	public async Task<IActionResult> GetContractRefMaxSerialNumberAsync(Guid id , CancellationToken cancellationToken)
	{
		var query = new GetContractRefMaxSerialNumberQuery(id);
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}

	[HttpGet("GetAttachedContractData/{id}")]
	public async Task<IActionResult> GetAttachedContractData(Guid id, CancellationToken cancellationToken)
	{
		var query = new GetContractAttachedDataQuery(id);
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
    }

    [HttpPost("create-contract")]
	public async Task<IActionResult> CreateContract(
		[FromForm] CreateContractDTO creatrContractDTO,
		CancellationToken cancellationToken)
	{
		try
		{
			var command = new CreateContractCommand(creatrContractDTO);
			await _mediator.Send(command, cancellationToken);
			return Ok(new ApiResponse { Success = true, Message = "Data has been saved successfully ✅" });
		}
		catch (Exception ex)
		{
			return BadRequest(new ApiResponse { Success = false, Message = $"An error occurred while saving the data: {ex.Message}" });
		}
	}

    [HttpGet("get-contracts-numbers")]
	public async Task<IActionResult> GetContractsNumbers(CancellationToken cancellationToken)
	{
		var query = new GetContractsNumbersQuery();
		var contracts = await _mediator.Send(query, cancellationToken);
		return Ok(contracts);
	}

	[HttpGet("get-contract/{id}")]
	public async Task<IActionResult> GetContractDocumentById(Guid Id, CancellationToken cancellationToken)
	{
		var query = new GetContractDocumentByIdQuery(Id);
		var contract = await _mediator.Send(query, cancellationToken);
		return Ok(contract);
    }

    [HttpPut("update-contract/{id}")]
    public async Task<IActionResult> UpdateContractDocumentById(
		Guid Id,
		[FromForm] UpdateContractDocumentRequestDTO updateContractDocumentDTO,
        CancellationToken cancellationToken)
    {
        var query = new UpdateContractDocumentCommand(Id , updateContractDocumentDTO);
        var contract = await _mediator.Send(query, cancellationToken);
        return Ok(contract);
    }

    [HttpDelete("deletecontract")]
	public async Task<IActionResult> DeleteContract([FromQuery]Guid Id, CancellationToken cancellationToken)
	{
		var command = new DeleteContractCommand(Id);
		await _mediator.Send(command, cancellationToken);
		return StatusCode(204);
	}
}