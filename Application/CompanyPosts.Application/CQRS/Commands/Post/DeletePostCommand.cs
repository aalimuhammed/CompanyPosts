namespace CompanyPost.Application.CQRS.Commands.Post;
public record DeletePostCommand(Guid Id) : IRequest<Unit>;