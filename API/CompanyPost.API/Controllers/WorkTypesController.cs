namespace CompanyPost.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkTypesController : ControllerBase
{
	private readonly IMediator _mediator;
	public WorkTypesController(IMediator mediator)
	{
		_mediator = mediator;
	}
	[HttpGet("get-worktypes")]
	public async Task<IActionResult> GetWorkTypes(CancellationToken cancellationToken)
	{
		var query = new GetWorkTypesQuery();
		var result = await _mediator.Send(query);
		return Ok(result);
	}
}
