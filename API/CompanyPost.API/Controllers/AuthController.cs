namespace CompanyPosts.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly IMediator _mediator;
	public AuthController(IMediator mediator)
	{
		_mediator = mediator;
	}
	[AllowAnonymous]
	[HttpPost("login")]
	public async Task<ActionResult<AuthResultDTO>> Login([FromBody] SysUserLoginQuery command)
	{
		var result = await _mediator.Send(command);
		return Ok(result);
	}
}
