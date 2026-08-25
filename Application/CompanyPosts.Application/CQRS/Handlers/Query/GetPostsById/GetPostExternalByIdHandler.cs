using CompanyPost.Application.DTO;
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

            var includes = new List<Expression<Func<PostExternal, object>>>
                    {
                        post => post.Attachments
                    };

            var post = await postRepository.FindWithIncludeFirstOrDefaultAsync(
                p => p.Id == request.Id, includes, cancellationToken);

            if (post == null)
            {
                throw new Exception($"Post with Id {request.Id} not found.");
            }

            var selectedPostDto = new SelectedPostByIdDTO(
                post.DocumentNumber,
                post.Subject,
                post.Summary,
                post.Notes,
                post.OldReferenceNumber,
                post.InComingNumber,
                post.CompanyId,
                post.PublishedId,
                post.RecievedFromId,
                post.WorkTypeId,
                post.DocumentDate,
                post.DeliveryDate,
                (int)post.DeliveryMethods,
                (int)post.Status,
                (int)post.PostDocumentTypes ,
                post.Attachments != null && post.Attachments.Any()
               ? post.Attachments.Select(a => new AttachmentDTO(a.Id, a.FileName!, $"/posts/{a.FileName}")).ToList()
              : new List<AttachmentDTO>());

            return selectedPostDto;
        }
    }
}