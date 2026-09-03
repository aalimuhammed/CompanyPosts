using CompanyPost.Application.CQRS.Commands.SysUser;

namespace CompanyPost.API.Controllers
{
    //[Authorize]
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
			([FromBody]CreateSysUserCompanyDTO createSysUserCompanyDTO ,
			CancellationToken cancellationToken)
		{
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateSysUserCompanyCommand(createSysUserCompanyDTO);

            try
            {
                await _mediator.Send(command, cancellationToken);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message,
                    errorType = "BusinessRuleViolation"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An unexpected error occurred.",
                    errorType = "UnhandledException",
                    details = ex.Message
                });
            }
        }

		[HttpGet("getfollowingpersons")]
		public async Task<IActionResult> GetFollowingPersons()
		{
			var query = new GetFollowingPersonsQuery();
			var results = await _mediator.Send(query);
			return Ok(results);
		}

        //[HttpPost("activate")]

    
    }
}