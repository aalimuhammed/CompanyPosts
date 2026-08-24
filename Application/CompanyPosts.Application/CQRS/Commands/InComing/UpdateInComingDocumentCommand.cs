namespace CompanyPost.Application.CQRS.Commands.InComing
{
    public record UpdateInComingDocumentCommand(
        Guid Id , 
        UpdateInComingDocumentRequestDTO UpdateInComingDocumentRequest) 
        : IRequest<bool>;
}