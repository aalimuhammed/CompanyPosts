using CompanyPost.Application.DTO.Response.Base;

namespace CompanyPost.Application.CQRS.Handlers.Query.GetPostsById
{
    internal sealed class GetPostExternalByIdHandler 
        : IRequestHandler<GetPostExternalByIdQuery, SelectedPostByIdDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPostExternalByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<SelectedPostByIdDTO> Handle(GetPostExternalByIdQuery request, CancellationToken cancellationToken)
        {
            var postRepository = _unitOfWork.Repository<PostExternal>();

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
                post.ReceivedFromSupplierId,
                post.WorkTypeId,
                post.DocumentDate,
                post.DeliveryDate,
                (int)post.Department,
                (int)post.DeliveryMethods);

            return selectedPostDto;
        }
    }
}