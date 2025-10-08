namespace CompanyPost.Application.CQRS.Commands.Posts;
public record CreatePostTransformerCommand
	(CreatePostTransofrmerDTO CreatePostTransofrmerDTO) 
	: IRequest<Unit>;