namespace CompanyPost.Application.CQRS.Query
{
   public record GetPostExternalByIdQuery(Guid Id) : IRequest<SelectedPostByIdDTO>;
}