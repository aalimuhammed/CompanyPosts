using CompanyPost.Application.DTO.Request.Base;

namespace CompanyPost.Application.CQRS.Query
{
    public record GetPostInternalDocumentsExcelReportQuery
        (BaseDocumentFilterRequestDTO BaseDocumentFilterRequestDTO)
        : IRequest<byte[]>;
}