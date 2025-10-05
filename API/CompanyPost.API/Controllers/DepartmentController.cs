namespace CompanyPost.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
	private readonly IMediator _mediator;
	public DepartmentController(IMediator mediator)
	{
		_mediator = mediator;
	}
	[HttpGet("get-departments")]
	public async Task<IActionResult> GetDepartments(CancellationToken cancellationToken)
	{
		var query = new GetDepartmentsQuery();
		var departments = await _mediator.Send(query, cancellationToken);
		return Ok(departments);
	}
}