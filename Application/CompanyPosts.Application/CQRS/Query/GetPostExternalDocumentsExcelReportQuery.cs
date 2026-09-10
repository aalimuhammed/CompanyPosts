using CompanyPost.Application.DTO.Request.Base;

namespace CompanyPost.Application.CQRS.Query
{
    public record GetPostExternalDocumentsExcelReportQuery
        (BaseDocumentFilterRequestDTO BaseDocumentFilterRequestDTO)
        : IRequest<byte[]>;
}
