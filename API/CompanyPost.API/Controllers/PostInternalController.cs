namespace CompanyPost.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostInternalController : ControllerBase
{
	private readonly IMediator _mediator;
	public PostInternalController(IMediator mediator)
	{
		_mediator = mediator;
	}
	[HttpGet("GetPostInternalMaxSerialNumber")]
	public async Task<IActionResult> GetPostInternalMaxSerialNumber(CancellationToken cancellationToken)
	{
		var query = new GetPostInternalMaxSerialNumberQuery();
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}
	[HttpPost("CreatePostInternal")]
	public async Task<IActionResult> CreatePostInternal(
		[FromForm] CreatePostInternalDTO createPostInternalDTO, 
		CancellationToken cancellationToken)
	{
		try
		{
			var command = new CreatePostInternalCommand(createPostInternalDTO);
			await _mediator.Send(command, cancellationToken);
			return Ok(new { Message = "تم الحفظ بنجاح ✅" });
		}
		catch (Exception ex)
		{
			return BadRequest(new { Message = $"حدث خطأ: {ex.Message}" });
		}
	}

}