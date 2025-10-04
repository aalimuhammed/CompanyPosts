namespace CompanyPosts.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
	private readonly IMediator _mediator;
	public PostsController(IMediator mediator)
	{
		_mediator = mediator;
	}
	[HttpPost("create-post")]
	public async Task<IActionResult> CreatePost(
		[FromForm] CreatePostDTO createPostDTO , 
		CancellationToken cancellationToken)
	{
		var command = new CreatePostCommand(createPostDTO);
		await _mediator.Send(command, cancellationToken);
		return Ok();
	}

	[HttpGet("get-posts")]
	public async Task<IActionResult> GetPosts(CancellationToken cancellationToken)
	{
		var query = new GetPostsQuery();
		var posts = await _mediator.Send(query , cancellationToken);
		return Ok(posts);
	}

	[HttpPut("update-post")]
	public async Task<IActionResult> UpdatePost(
		[FromForm] UpdatePostDTO updatePostDTO ,
		CancellationToken cancellationToken)
	{
		var command = new UpdatePostCommand(updatePostDTO);
		await _mediator.Send(command , cancellationToken);
		return StatusCode(204);
	}

	[HttpDelete("delete-post")]
	public async Task<IActionResult> DeletePost(Guid Id , CancellationToken cancellationToken)
	{
		var command = new DeletePostCommand(Id);
		await _mediator.Send(command, cancellationToken);
		return StatusCode(204);
	}
}