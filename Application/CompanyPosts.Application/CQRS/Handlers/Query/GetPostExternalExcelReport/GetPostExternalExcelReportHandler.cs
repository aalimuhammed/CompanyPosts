using CompanyPost.Application.CQRS.Services;

namespace CompanyPost.Application.CQRS.Handlers.Query.GetPostExternalExcelReport
{
    internal sealed class GetPostExternalExcelReportHandler : IRequestHandler<GetPostExternalDocumentsExcelReportQuery, byte[]>
    {
        private readonly IPostsExcelReportQueryService<PostExternal> _postsExcelReportQueryService;
        private readonly IExcelExportService<PostDocumentExcelDTO> _excelExportService;

        public GetPostExternalExcelReportHandler(
            IPostsExcelReportQueryService<PostExternal> postsExcelReportQueryService,
            IExcelExportService<PostDocumentExcelDTO> excelExportService)
        {
            _postsExcelReportQueryService = postsExcelReportQueryService;
            _excelExportService = excelExportService;
        }

        public async Task<byte[]> Handle(GetPostExternalDocumentsExcelReportQuery request,
            CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<PostExternal, object>>>
                 {
                     post => post.CreatedBy,
                     post => post.Publisher,
                     post => post.RecievedFrom,
                     post => post.Company,
                 };

            var predicate = PredicateBuilder.New<PostExternal>(true);

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
