using CompanyPost.Application.DTO.Request.Base;

namespace CompanyPost.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ExcelController(IMediator mediator) => _mediator = mediator;

        [HttpGet("post-internal")]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        public async Task<IActionResult> GetPostInternalDocumentsExcel(
            [FromQuery] BaseDocumentFilterRequestDTO dto , 
            CancellationToken cancellationToken = default)
        {
            var query = new GetPostInternalDocumentsExcelReportQuery(dto);
            var result = await _mediator.Send(query, cancellationToken);

            return File(
               result,
               "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
               "PostInternalExcelReport.xlsx"
           );
        }
    }
}