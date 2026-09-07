using CompanyPost.API.Model;
using CompanyPost.Application.CQRS.Commands.InComing;
using CompanyPost.Application.CQRS.Commands.PurchaseOrder;
using CompanyPost.Application.DTO.Request.Base;

namespace CompanyPost.API.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
	[ApiController]
	public class DocumentsController : ControllerBase
    {
        #region Constructor
        private readonly IMediator _mediator;
        public DocumentsController(IMediator mediator) => _mediator = mediator;
        #endregion


        #region Post External
        [HttpGet("post-external")]
        public async Task<IActionResult> GetPostExternalDocuments([FromQuery] BaseDocumentFilterRequestDTO dto)
            => Ok(await _mediator.Send(new GetPostExternalDocumentsQuery(dto)));

        [HttpGet("post-external/{id}")]
        public async Task<IActionResult> GetPostExternalById(Guid id, CancellationToken ct)
            => Ok(await _mediator.Send(new GetPostExternalByIdQuery(id), ct));

        [HttpPut("post-external/{id}")]
        public async Task<IActionResult> UpdatePostExternalDocument(Guid id, [FromForm] UpdatePostExternalDocumentRequestDTO dto, CancellationToken ct)
            => Ok(await _mediator.Send(new UpdatePostExternalDocumentCommand(id, dto), ct));

        [HttpDelete("post-external/{id}")]
        public Task<IActionResult> DeletePostExternalDocument(Guid id, CancellationToken ct)
            => Task.FromResult<IActionResult>(NoContent());
        #endregion


        #region Post Internal
        [HttpGet("post-internal")]
        public async Task<IActionResult> GetPostInternalDocuments([FromQuery] BaseDocumentFilterRequestDTO dto)
            => Ok(await _mediator.Send(new GetPostInternalDocumentsQuery(dto)));

        [HttpGet("post-internal/{id}")]
        public async Task<IActionResult> GetPostInternalById(Guid id, CancellationToken ct)
            => Ok(await _mediator.Send(new GetPostInternalByIdQuery(id), ct));

        [HttpPut("post-internal/{id}")]
        public async Task<IActionResult> UpdatePostInternalDocument(Guid id, [FromForm] UpdatePostInternalDocumentRequestDTO dto, CancellationToken ct)
            => Ok(await _mediator.Send(new UpdatePostInternalDocumentCommand(id, dto), ct));
        #endregion


        #region Post Transformer
        [HttpGet("post-transformer")]
        public async Task<IActionResult> GetPostTransformerDocuments([FromQuery] BaseDocumentFilterRequestDTO dto)
            => Ok(await _mediator.Send(new GetPostTransformerDocumentsQuery(dto)));

        [HttpGet("post-transformer/{id}")]
        public async Task<IActionResult> GetPostTransformerById(Guid id, CancellationToken ct)
            => Ok(await _mediator.Send(new GetPostTransformerByIdQuery(id), ct));

        [HttpPut("post-transformer/{id}")]
        public async Task<IActionResult> UpdatePostTransformerDocuments(Guid id, [FromForm] UpdatePostTransformerDocumentRequestDTO dto, CancellationToken ct)
            => Ok(await _mediator.Send(new UpdatePostTransformerDocumentCommand(id, dto), ct));
        #endregion


        #region Incoming Documents
        [HttpGet("incoming")]
        public async Task<IActionResult> GetIncomingDocuments([FromQuery] BaseDocumentFilterRequestDTO dto)
            => Ok(await _mediator.Send(new GetInComingDocumentsQuery(dto)));

        [HttpGet("incoming/{id}")]
        public async Task<IActionResult> GetIncomingById(Guid id, CancellationToken ct)
            => Ok(await _mediator.Send(new GetInComingByIdQuery(id), ct));

        [HttpPut("incoming/{id}")]
        public async Task<IActionResult> UpdateIncomingDocument(Guid id, [FromForm] UpdateInComingDocumentRequestDTO dto, CancellationToken ct)
            => Ok(await _mediator.Send(new UpdateInComingDocumentCommand(id, dto), ct));
        #endregion


        #region Contracts
        [HttpGet("contracts")]
        public async Task<IActionResult> GetContractsByFilters([FromQuery] ContractsFilterRequestDTO dto, CancellationToken ct)
            => Ok(await _mediator.Send(new GetContractsByFiltersQuery(dto), ct));
        #endregion


        #region Purchase Orders
        [HttpGet("purchase-orders")]
        public async Task<IActionResult> GetPurchaseOrdersByFilters([FromQuery] PurchaseOrderFilterRequestDTO dto, CancellationToken ct)
            => Ok(await _mediator.Send(new GetPurchaseOrderByFiltersQuery(dto), ct));

        [HttpGet("purchase-orders/{id}")]
        public async Task<IActionResult> GetPurchaseOrdersById(Guid id, CancellationToken ct)
            => Ok(await _mediator.Send(new GetPurchaseOrderByIdQuery(id), ct));

        [HttpPut("purchase-orders/{id}")]
        public async Task<IActionResult> UpdatePurchaseOrderDocument(Guid id, [FromForm] UpdatePurchaseOrderDocumentRequestDTO dto, CancellationToken ct)
            => Ok(await _mediator.Send(new UpdatePurchaseOrderDocumentCommand(id, dto), ct));

        [HttpDelete("purchase-orders/{id}")]
        public async Task<IActionResult> DeletePurchaseOrder(Guid id, CancellationToken ct)
        {
            try
            {
                var result = await _mediator.Send(new DeletePurchaseOrderCommand(id), ct);

                return result
                    ? Ok(new ApiResponse { Success = true, Message = "Purchase order deleted successfully ✅" })
                    : NotFound(new ApiResponse { Success = false, Message = "Purchase order not found ❌" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse
                    {
                        Success = false,
                        Message = $"An unexpected error occurred: {ex.Message}"
                    });
            }
        }
        #endregion
    }
}