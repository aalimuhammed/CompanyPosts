using CompanyPost.API.Model;
using CompanyPost.Application.CQRS.Commands.PurchaseOrder;

namespace CompanyPost.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PurchaseOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetPurchaseOrderMaxSerialNumber")]
        public async Task<IActionResult> GetPurchaseOrderMaxSerialNumber(CancellationToken cancellationToken)
        {
            var query = new GetPurchaseOrderMaxSerialNumberQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost("createpurchaseorder")]
        public async Task<IActionResult> CreatePurchaseOrder(
            [FromForm] CreatePurchaseOrderDTO  createPurchaseOrderDTO , 
            CancellationToken cancellationToken)
        {
            try
            {
                var command = new CreatePurchaseOrderCommand(createPurchaseOrderDTO);
                await _mediator.Send(command, cancellationToken);
                return Ok(new ApiResponse { Success = true, Message = "Data has been saved successfully ✅" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse { Success = false, Message = $"{ex.Message}" });
            }
        }
    }
}