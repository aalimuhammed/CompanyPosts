using CompanyPost.Application.CQRS.Handlers.Query.Base;

namespace CompanyPost.Application.CQRS.Handlers.Query.CopyPosts
{
    internal class GetPostExternalToBeCopiedHandler : GetPostToBeCopiedBaseHandler<PostExternal , GetPostExternalToBeCopiedQuery>
    {
        public GetPostExternalToBeCopiedHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}