namespace CompanyPost.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PersonOrgController : ControllerBase
	{
		private readonly IMediator _mediator;
		public PersonOrgController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpGet("get-person-orgs")]
		public async Task<IActionResult> GetPersonOrgs()
		{
			var query = new GetPersonOrgsQuery();
			var personOrgs = await _mediator.Send(query);
			return Ok(personOrgs);
		}
	}
}