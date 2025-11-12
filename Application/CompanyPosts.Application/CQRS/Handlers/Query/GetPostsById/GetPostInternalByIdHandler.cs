namespace CompanyPost.Application.CQRS.Handlers.Query.GetPostsById
{
    internal sealed class GetPostInternalByIdHandler
        : IRequestHandler<GetPostInternalByIdQuery, SelectedPostByIdDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPostInternalByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<SelectedPostByIdDTO> Handle(GetPostInternalByIdQuery request, CancellationToken cancellationToken)
        {
            var postRepository = _unitOfWork.Repository<PostInternal>();

            var post = await postRepository.FindAsync(p => p.Id == request.Id, cancellationToken);

            if (post == null)
            {
                throw new Exception($"Post with Id {request.Id} not found.");
            }
            var selectedPostDto = new SelectedPostByIdDTO(
                post.DocumentNumber,
                post.Subject,
                post.Summary,
                post.Notes,
                post.CompanyId,
                post.PublishedId,
                post.RecievedFromId,
                post.WorkTypeId,
                post.DocumentDate,
                post.DeliveryDate,
                (int)post.Department,
                (int)post.DeliveryMethods);

            return selectedPostDto;
        }
    }
}
