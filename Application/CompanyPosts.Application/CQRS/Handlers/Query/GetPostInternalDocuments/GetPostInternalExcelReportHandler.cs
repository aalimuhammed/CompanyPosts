using CompanyPost.Application.CQRS.Services;

namespace CompanyPost.Application.CQRS.Handlers.Query.GetPostInternalDocuments
{
    internal sealed class GetPostInternalExcelReportHandler : IRequestHandler<GetPostInternalDocumentsExcelReportQuery, byte[]>
    {
        private readonly IPostsExcelReportQueryService<PostInternal> _postsExcelReportQueryService;
        private readonly IExcelExportService<PostDocumentExcelDTO> _excelExportService;
        public GetPostInternalExcelReportHandler(
            IPostsExcelReportQueryService<PostInternal> postsExcelReportQueryService, 
            IExcelExportService<PostDocumentExcelDTO> excelExportService)
        {
            _postsExcelReportQueryService = postsExcelReportQueryService;
            _excelExportService = excelExportService;
        }
        public async Task<byte[]> Handle(
            GetPostInternalDocumentsExcelReportQuery request, 
            CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<PostInternal, object>>>
                 {
                     post => post.CreatedBy,
                     post => post.Publisher,
                     post => post.RecievedFrom,
					 post => post.Company,
                     post => post.Attachments,
                 };

            var predicate = PredicateBuilder.New<PostInternal>(true);

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