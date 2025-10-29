namespace CompanyPost.Application.CQRS.Query;
public record GetPostExternalQuery : IRequest<IEnumerable<PostResponseDTO>>;