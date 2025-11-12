using CompanyPost.Application.DTO.Request.Base;

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
		public async Task<IActionResult> GetPostExternalDocuments([FromQuery] BaseDocumentFilterRequestDTO baseDocumentFilterRequestDTO)
		{
			var query = new GetPostExternalDocumentsQuery(baseDocumentFilterRequestDTO);
			var documents = await _mediator.Send(query);
			return Ok(documents);
		}

        [HttpGet("post-external/{id}")]
        public async Task<IActionResult> GetPostExternalById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetPostExternalByIdQuery(id);
            var post = await _mediator.Send(query, cancellationToken);
            return Ok(post);
        }

        [HttpPut("post-external/{id}")]
        public async Task<IActionResult> UpdatePostExternalDocument(
            Guid id, 
            [FromBody] UpdatePostExternalDocumentRequestDTO updateRequestDTO, 
            CancellationToken cancellationToken)
        {
            var command = new UpdatePostExternalDocumentCommand(id, updateRequestDTO);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("post-external/{id}")]
        public async Task<IActionResult> DeletePostExternalDocument(Guid id, CancellationToken cancellationToken)
        {
            //var command = new DeletePostExternalDocumentCommand(id);
            //await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpGet("post-internal")]
		public async Task<IActionResult> GetPostInternalDocuments([FromQuery] BaseDocumentFilterRequestDTO baseDocumentFilterRequestDTO)
		{
			var query = new GetPostInternalDocumentsQuery(baseDocumentFilterRequestDTO);
			var documents = await _mediator.Send(query);
			return Ok(documents);
		}

        [HttpGet("post-internal/{id}")]
        public async Task<IActionResult> GetPostInternalById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetPostInternalByIdQuery(id);
            var post = await _mediator.Send(query, cancellationToken);
            return Ok(post);
        }

        [HttpPut("post-internal/{id}")]
        public async Task<IActionResult> UpdatePostInternalDocument(
            Guid id, 
            [FromBody] UpdatePostInternalDocumentRequestDTO updatePostExternalDocumentRequestDTO, 
            CancellationToken cancellationToken)
        {
            var query = new UpdatePostInternalDocumentCommand(id , updatePostExternalDocumentRequestDTO);
            var details = await _mediator.Send(query, cancellationToken);
            return Ok(details);
        }

        [HttpGet("post-transformer")]
		public async Task<IActionResult> GetPostTransformerDocuments([FromQuery] BaseDocumentFilterRequestDTO baseDocumentFilterRequestDTO)
		{
			var query = new GetPostTransformerDocumentsQuery(baseDocumentFilterRequestDTO);
			var documents = await _mediator.Send(query);
			return Ok(documents);
		}

        [HttpGet("post-transformer/{id}")]
        public async Task<IActionResult> GetPostTransformerById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetPostTransformerByIdQuery(id);
            var post = await _mediator.Send(query, cancellationToken);
            return Ok(post);
        }

        [HttpGet("incoming")]
		public async Task<IActionResult> GetInComingDocuments([FromQuery] BaseDocumentFilterRequestDTO baseDocumentFilterRequestDTO)
		{
			var query = new GetInComingDocumentsQuery(baseDocumentFilterRequestDTO);
			var documents = await _mediator.Send(query);
			return Ok(documents);
		}

        [HttpGet("incoming/{id}")]
        public async Task<IActionResult> GetInComingById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetInComingByIdQuery(id);
            var incoming = await _mediator.Send(query, cancellationToken);
            return Ok(incoming);
        }

        [HttpGet("get-contracts")]
        public async Task<IActionResult> GetContractsByFilters([FromQuery] ContractsFilterRequestDTO filterDTO, CancellationToken cancellationToken)
        {
            var query = new GetContractsByFiltersQuery(filterDTO);
            var contracts = await _mediator.Send(query, cancellationToken);
            return Ok(contracts);
        }

		[HttpGet("get-purchase-orders")]
		public async Task<IActionResult> GetPurchaseOrdersByFilters([FromQuery] PurchaseOrderFilterRequestDTO filterDTO, CancellationToken cancellationToken)
		{
			var query = new GetPurchaseOrderByFiltersQuery(filterDTO);
			var purchaseOrders = await _mediator.Send(query, cancellationToken);
			return Ok(purchaseOrders);
        }
    }
}