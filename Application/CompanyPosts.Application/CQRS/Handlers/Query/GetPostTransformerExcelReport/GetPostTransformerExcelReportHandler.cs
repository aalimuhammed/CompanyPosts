using CompanyPost.Application.CQRS.Services;

namespace CompanyPost.Application.CQRS.Handlers.Query.GetPostTransformerExcelReport
{
    internal sealed class GetPostTransformerExcelReportHandler : IRequestHandler<GetPostTransformerDocumentsExcelReportQuery, byte[]>
    {
        private readonly IPostsExcelReportQueryService<PostTransformer> _postsExcelReportQueryService;
        private readonly IExcelExportService<PostDocumentExcelDTO> _excelExportService;

        public GetPostTransformerExcelReportHandler(
            IPostsExcelReportQueryService<PostTransformer> postsExcelReportQueryService,
            IExcelExportService<PostDocumentExcelDTO> excelExportService)
        {
            _postsExcelReportQueryService = postsExcelReportQueryService;
            _excelExportService = excelExportService;
        }

        public async Task<byte[]> Handle(GetPostTransformerDocumentsExcelReportQuery request,
            CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<PostTransformer, object>>>
                 {
                     post => post.CreatedBy,
                     post => post.Publisher,
                     post => post.RecievedFrom,
                     post => post.Company,
                 };

            var predicate = PredicateBuilder.New<PostTransformer>(true);

            var result = await _postsExcelReportQueryService.GetPostDocumentsAsync(
                request.BaseDocumentFilterRequestDTO,
                predicate,
                includes,
                cancellationToken);

            var excelReport = _excelExportService.ExportToExcel(result);

            return excelReport;
        }
    }
}
