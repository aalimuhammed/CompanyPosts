namespace CompanyPost.Application.CQRS.Commands.Contract
{
    public record UpdateContractDocumentCommand(
        Guid Id, 
        UpdateContractDocumentRequestDTO UpdateContractDocumentDTO) 
        : IRequest<bool>;
}