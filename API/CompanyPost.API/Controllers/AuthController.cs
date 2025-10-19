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

	[HttpGet("verify-token")]
	[Authorize]
	public async Task<IActionResult> VerifyToken()
	{
		var query = new VerifyTokenQuery();
		var result = await _mediator.Send(query);
		if (!result.IsValid)
			return Unauthorized(result);

		return Ok(result);
	}
}