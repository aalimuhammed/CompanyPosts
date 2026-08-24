using CompanyPost.Application.DTO;
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

            var includes = new List<Expression<Func<PostTransformer, object>>>
                    {
                        post => post.Attachments,
                    };

            var post = await postRepository.FindWithIncludeFirstOrDefaultAsync(
                p => p.Id == request.Id, includes, cancellationToken);

            if (post == null)
            {
                throw new Exception($"Post with Id {request.Id} not found.");
            }

            var selectedPostDto = new SelectedPostTransformerByIdDTO(
                post.DocumentNumber,
                post.Subject,
                post.Summary,
                post.Notes,
                post.OldReferenceNumber,
                post.InComingNumber,
                post.CompanyId,
                post.PublishedId,
                post.RecievedFromId,   //received
				post.WorkTypeId,
                post.DocumentDate,
                post.DeliveryDate,
                (int)post.DeliveryMethods ,
                 (int)post.Status,
                post.PostNumber,
               // post.RecivedByName,
                (int)post.PostDocumentTypes ,
                (int)post.DocumentType , 
                 post.Attachments != null && post.Attachments.Any()
               ? post.Attachments.Select(a => new AttachmentDTO(a.Id, a.FileName!, $"/posts/{a.FileName}")).ToList()
              : new List<AttachmentDTO>());

                return selectedPostDto;
        }
    }
}