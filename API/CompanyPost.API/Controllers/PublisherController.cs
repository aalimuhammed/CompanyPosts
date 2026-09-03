namespace CompanyPost.API.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PublisherController : ControllerBase
{
	private readonly IMediator _mediator;
	public PublisherController(IMediator mediator)
	{
		_mediator = mediator;
	}
	
	[HttpGet("get-projects")]
	public async Task<IActionResult> GetProjects(CancellationToken cancellationToken)
	{
		var query = new GetProjectsQuery();
		var projects = await _mediator.Send(query, cancellationToken);
		return Ok(projects);
	}

	[HttpGet("projects-departments")]
	public async Task<IActionResult> GetPorjectsAndDepartments(CancellationToken cancellationToken)
	{
		var query = new GetProjectsAndDepartmentsQuery();
		var projectAndDepartments = await _mediator.Send(query, cancellationToken);
		return Ok(projectAndDepartments);
	}

	[HttpGet("get-suppliers")]
	public async Task<IActionResult> GetSuppliers(CancellationToken cancellationToken)
	{
		var query = new GetSuppliersQuery();
		var suppliers = await _mediator.Send(query,cancellationToken);
		return Ok(suppliers);
	}

	[HttpGet("get-companies")]
	public async Task<IActionResult> GetCompanies(CancellationToken cancellationToken)
	{
		var query = new GetPublisherCompaniesQuery();
		var companies = await _mediator.Send(query, cancellationToken);
		return Ok(companies);
	}
}