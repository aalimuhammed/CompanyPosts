using CompanyPost.API.Model;

namespace CompanyPost.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostExternalController : ControllerBase
{
	private readonly IMediator _mediator;
	public PostExternalController(IMediator mediator)
	{
		_mediator = mediator;
	}
	[HttpGet("GetPostExternalMaxSerialNumber")]
	public async Task<IActionResult> GetPostExternalMaxSerialNumber(CancellationToken cancellationToken)
	{
		var query = new GetPostExternalMaxSerialNumberQuery();
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}
	[HttpPost("CreatePostExternal")]
	public async Task<IActionResult> CreatePostExternal(
		[FromForm] CreatePostExternalDTO createPostExternalDTO,
		CancellationToken cancellationToken)
	{
		try
		{
			var command = new CreatePostExternalCommand(createPostExternalDTO);
			await _mediator.Send(command, cancellationToken);
			return Ok(new ApiResponse { Success = true, Message = "Data has been saved successfully ✅" });
		}
		catch (Exception ex)
		{
			return BadRequest(new ApiResponse { Success = false, Message = $"An error occurred while saving the data: {ex.Message}" });
		}
	}
}
