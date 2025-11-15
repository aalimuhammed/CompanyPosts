namespace CompanyPost.Application.CQRS.Query
{
    public record GetContractDocumentByIdQuery(Guid Id) : IRequest<GetContractByIdResponseDTO>;
}