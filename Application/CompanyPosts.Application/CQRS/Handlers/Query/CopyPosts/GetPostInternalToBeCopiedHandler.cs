using CompanyPost.Application.CQRS.Handlers.Query.Base;

namespace CompanyPost.Application.CQRS.Handlers.Query.CopyPosts
{
    internal class GetPostInternalToBeCopiedHandler : GetPostToBeCopiedBaseHandler<PostInternal, GetPostInternalToBeCopiedQuery>
    {
        public GetPostInternalToBeCopiedHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}