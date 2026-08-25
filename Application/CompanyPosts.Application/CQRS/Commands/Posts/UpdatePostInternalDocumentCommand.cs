namespace CompanyPost.Application.CQRS.Commands.Posts
{
    public record UpdatePostInternalDocumentCommand(
        Guid Id , 
        UpdatePostInternalDocumentRequestDTO UpdatePostInternalDocumentRequestDTO) 
        : IRequest<bool>;
}