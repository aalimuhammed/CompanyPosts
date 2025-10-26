namespace CompanyPost.Application.CQRS.Query;
public record GetFollowingPersonsQuery : IRequest<IEnumerable<FollowingPersonsDTO>>;