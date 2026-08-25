namespace CompanyPost.Application.CQRS.Commands.Posts
{
    public record UpdatePostTransformerDocumentCommand
        (Guid Id, UpdatePostTransformerDocumentRequestDTO UpdatePostTransformerDocumentRequestDTO) 
        : IRequest<bool>;
}