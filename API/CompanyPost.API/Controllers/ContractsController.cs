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
	[HttpPost("create-contract")]
	public async Task<IActionResult> CreateContract(
		[FromForm] CreateContractDTO creatrContractDTO,
		CancellationToken cancellationToken)
	{
		var command = new CreateContractCommand(creatrContractDTO);
		await _mediator.Send(command, cancellationToken);
		return Ok(new { Message = "Contract created successfully" });
	}
	[HttpGet("get-contracts")]
	public async Task<IActionResult> GetContracts(CancellationToken cancellationToken)
	{
		var query = new GetContractsQuery();
		var contracts = await _mediator.Send(query, cancellationToken);
		return Ok(contracts);
	}
	[HttpPut("update-contract")]
	public async Task<IActionResult> UpdateContract(
		[FromForm] UpdateContractDTO updateContractDTO,
		CancellationToken cancellationToken)
	{
		var command = new UpdateContractCommand(updateContractDTO);
		await _mediator.Send(command, cancellationToken);
		return StatusCode(204);
	}

	[HttpDelete("delete-contract")]
	public async Task<IActionResult> DeleteContract([FromQuery]Guid Id, CancellationToken cancellationToken)
	{
		var command = new DeleteContractCommand(Id);
		await _mediator.Send(command, cancellationToken);
		return StatusCode(204);
	}
}