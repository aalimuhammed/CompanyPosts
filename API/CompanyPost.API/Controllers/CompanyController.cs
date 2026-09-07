namespace CompanyPost.API.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
	private readonly IMediator _mediator;
	public CompanyController(IMediator mediator)
	{
		_mediator = mediator;
	}
	[HttpGet("get-companies")]
	public async Task<IActionResult> GetCompanies(CancellationToken cancellationToken)
	{
		var query = new GetCompaniesQuery();
		var companies = await _mediator.Send(query, cancellationToken);
		return Ok(companies);
	}
}