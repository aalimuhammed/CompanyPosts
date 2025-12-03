using CompanyPost.Application.CQRS.Handlers.Query.Base;

namespace CompanyPost.Application.CQRS.Handlers.Query.CopyPosts
{
    internal class GetPostTransformerToBeCopiedHandler : GetPostToBeCopiedBaseHandler<PostTransformer, GetPostTransformerToBeCopiedQuery>
    {
        public GetPostTransformerToBeCopiedHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}