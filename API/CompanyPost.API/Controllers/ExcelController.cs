using CompanyPost.Application.Abstraction;
using CompanyPost.Application.DTO;
using CompanyPost.Application.DTO.Request.Base;
using CompanyPost.Infrastructure.Services;

namespace CompanyPost.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IExcelExportService<ContractExcelRowModel> _excelExportService;

        public ExcelController(
            IMediator mediator ,
            IExcelExportService<ContractExcelRowModel> excelExportServic)
        {
            _mediator = mediator;
            _excelExportService = excelExportServic;
        }

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

        [HttpGet("post-external")]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        public async Task<IActionResult> GetPostEternalDocumentsExcel(
            [FromQuery] BaseDocumentFilterRequestDTO dto,
            CancellationToken cancellationToken = default)
        {
            var query = new GetPostExternalDocumentsExcelReportQuery(dto);
            var result = await _mediator.Send(query, cancellationToken);

            return File(
               result,
               "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
               "PostExternalExcelReport.xlsx"
           );
        }

        [HttpGet("post-transformer")]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        public async Task<IActionResult> GetPostTransformerDocumentsExcel(
            [FromQuery] BaseDocumentFilterRequestDTO dto,
            CancellationToken cancellationToken = default)
        {
            var query = new GetPostTransformerDocumentsExcelReportQuery(dto);
            var result = await _mediator.Send(query, cancellationToken);

            return File(
               result,
               "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
               "PostTransformerExcelReport.xlsx"
           );
        }

        [HttpGet("incomig")]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        public async Task<IActionResult> GetInComingPostTransformerDocumentsExcel(
            [FromQuery] BaseDocumentFilterRequestDTO dto,
            CancellationToken cancellationToken = default)
        {
            var query = new GetIncomingExcelReportQuery(dto);
            var result = await _mediator.Send(query, cancellationToken);

            return File(
               result,
               "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
               "IncomingPostExcelReport.xlsx"
           );
        }

        [HttpGet("contracts")]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        public async Task<IActionResult> GetContractsDocumentExcel(
        [FromQuery] ContractsFilterRequestDTO dto,
        CancellationToken ct)
        {
            var result = await _mediator.Send(
                new GetContractsByFiltersQuery(dto),
                ct);

            var mapping = result.Select(x => new ContractExcelRowModel(
                Project: x.Project,
                ContractNum: x.ContractNum,
                SerialNum: x.SerialNum,
                WorkType: x.WorkType,
                ContractDate: x.ContractDate.ToString(),
                Department: x.Department,
                PurchaseOrderRef: x.PurchaseOrderRef,
                Currency: x.Currency,
                PersonOrg: x.PersonOrg,
                Type: x.Type,
                CreatedBy: x.CreatedBy,
                CreatedAt: x.CreatedAt.ToString(),
                Value: x.Value,
                ApprovalDeliveryDate: x.ApprovalDeliveryDate,
                DateOfReceipt: x.DateOfReceipt
            )).ToList();

            var excelFile = _excelExportService.ExportToExcel(mapping);

            return File(
                excelFile,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "ContractsExcelReport.xlsx"
            );
        }
    }
}