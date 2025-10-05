namespace CompanyPost.Application.CQRS.Commands.Posts;
public record CreatePostExternalCommand 
	(CreatePostExternalDTO CreatePostExternalDTO) : IRequest<Unit>;