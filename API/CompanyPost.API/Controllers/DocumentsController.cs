namespace CompanyPost.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DocumentsController : ControllerBase
	{
		private readonly IMediator _mediator;
		public DocumentsController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpGet("post-external")]
		public async Task<IActionResult> GetPostExternalDocuments()
		{
			var query = new GetPostExternalDocumentsQuery();
			var documents = await _mediator.Send(query);
			return Ok(documents);
		}

		[HttpGet("post-internal")]
		public async Task<IActionResult> GetPostInternalDocuments()
		{
			var query = new GetPostInternalDocumentsQuery();
			var documents = await _mediator.Send(query);
			return Ok(documents);
		}

		[HttpGet("post-transformer")]
		public async Task<IActionResult> GetPostTransformerDocuments()
		{
			var query = new GetPostTransformerDocumentsQuery();
			var documents = await _mediator.Send(query);
			return Ok(documents);
		}

		[HttpGet("incoming")]
		public async Task<IActionResult> GetInComingDocuments()
		{
			var query = new GetInComingDocumentsQuery();
			var documents = await _mediator.Send(query);
			return Ok(documents);
		}
	}
}
