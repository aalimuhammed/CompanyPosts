using CompanyPost.Application.CQRS.Commands.SysUser;

namespace CompanyPost.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SysUsersController : ControllerBase
	{
		private readonly IMediator _mediator;
		public SysUsersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("createCompanyUser")]
		public async Task<IActionResult> CreateSysUserCompany
			(CreateSysUserCompanyDTO createSysUserCompanyDTO ,
			CancellationToken cancellationToken)
		{
			var command = new CreateSysUserCompanyCommand(createSysUserCompanyDTO);
			await _mediator.Send(command);
			return NoContent();
		}

		[HttpGet("getfollowingpersons")]
		public async Task<IActionResult> GetFollowingPersons()
		{
			var query = new GetFollowingPersonsQuery();
			var results = await _mediator.Send(query);
			return Ok(results);
		}
	}
}