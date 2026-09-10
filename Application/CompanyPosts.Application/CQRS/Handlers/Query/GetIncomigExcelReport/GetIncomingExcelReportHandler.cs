using CompanyPost.Application.CQRS.Services;
namespace CompanyPost.Application.CQRS.Handlers.Query.GetIncomigExcelReport
{
    internal sealed class GetIncomingExcelReportHandler : IRequestHandler<GetIncomingExcelReportQuery, byte[]>
    {
        private readonly IInComingPostsExcelReportQueryService _incomingsExcelReportQueryService;
        private readonly IExcelExportService<InComingPostDocumentExcelDTO> _excelExportService;

        public GetIncomingExcelReportHandler(
            IInComingPostsExcelReportQueryService incomingsExcelReportQueryService,
            IExcelExportService<InComingPostDocumentExcelDTO> excelExportService)
        {
            _incomingsExcelReportQueryService = incomingsExcelReportQueryService;
            _excelExportService = excelExportService;
        }
        public async Task<byte[]> Handle(GetIncomingExcelReportQuery request,
            CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<InComing, object>>>
                 {
                     incoming => incoming.CreatedBy,
                     incoming => incoming.Publisher,
                 };

            var predicate = PredicateBuilder.New<InComing>(true);

            var result = await _incomingsExcelReportQueryService.GetInComingPostDocumentsAsync(
                request.BaseDocumentFilterRequestDTO,
                predicate,
                includes,
                cancellationToken);

            var excelReport = _excelExportService.ExportToExcel(result);

            return excelReport;
        }
    }
}
