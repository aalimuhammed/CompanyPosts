using CompanyPost.Application.DTO.Request.Base;

namespace CompanyPost.Application.CQRS.Query
{
    public record GetPostTransformerDocumentsExcelReportQuery
        (BaseDocumentFilterRequestDTO BaseDocumentFilterRequestDTO)
        : IRequest<byte[]>;
}
