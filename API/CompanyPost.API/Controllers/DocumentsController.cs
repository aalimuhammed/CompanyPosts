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

		[HttpGet("post-internal")]
		public async Task<IActionResult> GetPostInternalDocuments([FromQuery] BaseDocumentFilterRequestDTO baseDocumentFilterRequestDTO)
		{
			var query = new GetPostInternalDocumentsQuery(baseDocumentFilterRequestDTO);
			var documents = await _mediator.Send(query);
			return Ok(documents);
		}

		[HttpGet("post-transformer")]
		public async Task<IActionResult> GetPostTransformerDocuments([FromQuery] BaseDocumentFilterRequestDTO baseDocumentFilterRequestDTO)
		{
			var query = new GetPostTransformerDocumentsQuery(baseDocumentFilterRequestDTO);
			var documents = await _mediator.Send(query);
			return Ok(documents);
		}

		[HttpGet("incoming")]
		public async Task<IActionResult> GetInComingDocuments([FromQuery] BaseDocumentFilterRequestDTO baseDocumentFilterRequestDTO)
		{
			var query = new GetInComingDocumentsQuery(baseDocumentFilterRequestDTO);
			var documents = await _mediator.Send(query);
			return Ok(documents);
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
