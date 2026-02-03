using CompanyPost.Application.DTO.Response.Base;

namespace CompanyPost.Application.CQRS.Handlers.Query.GetPostsById
{
    internal sealed class GetPostTransformerByIdHandler
        : IRequestHandler<GetPostTransformerByIdQuery, SelectedPostByIdDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPostTransformerByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<SelectedPostByIdDTO> Handle(GetPostTransformerByIdQuery request, CancellationToken cancellationToken)
        {
            var postRepository = _unitOfWork.Repository<PostTransformer>();

            var post = await postRepository.FindAsync(p => p.Id == request.Id, cancellationToken);

            if (post == null)
            {
                throw new Exception($"Post with Id {request.Id} not found.");
            }

            var selectedPostDto = new SelectedPostTransformerByIdDTO(
                post.DocumentNumber,
                post.Subject,
                post.Summary,
                post.Notes,
                post.CompanyId,
                post.PublishedId,
                post.RecievedFromId,   //received
				post.WorkTypeId,
                post.DocumentDate,
                post.DeliveryDate,
                (int)post.DeliveryMethods , 
                post.IncomingNumber,
                post.PostNumber,
                post.RecivedByName,
                (int)post.PostDocumentTypes ,
                (int)post.DocumentType);

                return selectedPostDto;
        }
    }
}