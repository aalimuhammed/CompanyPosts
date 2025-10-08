namespace CompanyPost.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostTransformerController : ControllerBase
{
	private readonly IMediator _mediator;
	public PostTransformerController(IMediator mediator)
	{
		_mediator = mediator;
	}
	[HttpGet("GetPostTransformerMaxSerialNumber")]
	public async Task<IActionResult> GetPostTransformerMaxSerialNumber(CancellationToken cancellationToken)
	{
		var query = new GetPostTransformerMaxSerialNumberQuery();
		var result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}
	[HttpPost("CreatePostTransformer")]
	public async Task<IActionResult> CreatePostTransformer(
		[FromForm] CreatePostTransofrmerDTO createPostTransofrmerDTO,
		CancellationToken cancellationToken)
	{
		var command = new CreatePostTransformerCommand(createPostTransofrmerDTO);
		await _mediator.Send(command, cancellationToken);
		return Ok();
	}
}