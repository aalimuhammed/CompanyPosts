using CompanyPost.Application.DTO.Response.Base;

namespace CompanyPost.Application.CQRS.Handlers.Query.Base
{
    internal class GetPostToBeCopiedBaseHandler<TEntity , TQuery> 
        : IRequestHandler<TQuery, PostsToCopyFromDTO>
        where TEntity : PostBaseEntity
        where TQuery : IRequest<PostsToCopyFromDTO> , IHasId
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPostToBeCopiedBaseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PostsToCopyFromDTO> Handle(TQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<TEntity>();

            Expression<Func<TEntity, object>>[] includes = 
                { c => c.Publisher , c => c.RecievedFrom , c => c.WorkType };

            var post = await repo.FindWithIncludeFirstOrDefaultAsync(
                x => x.Id == request.Id , includes , cancellationToken);

            var publisherType = post.Publisher switch
            {
                { IsDepartment: true } => "Department",
                { IsProject: true } => "Project",
                _ => ""
            };

            var copiedPost = new PostsToCopyFromDTO
                (
                    post.Notes,
                    post.Subject,
                    post.Summary,
                    post.CompanyId,
                    post.PublishedId,
                    post.RecievedFromId,
                    post.WorkTypeId,
                    post.DocumentDate,
                    post.DeliveryDate,
                    publisherType,
                    (int)post.PostDocumentTypes,
                    (int)post.DeliveryMethods
                );

            return copiedPost;
        }
    }
}