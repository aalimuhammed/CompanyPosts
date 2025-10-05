namespace CompanyPost.Application.CQRS.Commands.Posts;
public record CreatePostInternalCommand(CreatePostInternalDTO CreatePostInternalDTO) : IRequest<Unit>;