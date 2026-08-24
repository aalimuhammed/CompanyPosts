namespace CompanyPost.Application.CQRS.Commands.Posts
{
    public record UpdatePostExternalDocumentCommand(Guid Id , 
        UpdatePostExternalDocumentRequestDTO UpdatePostExternalDocumentRequestDTO) 
        : IRequest<bool>;
}