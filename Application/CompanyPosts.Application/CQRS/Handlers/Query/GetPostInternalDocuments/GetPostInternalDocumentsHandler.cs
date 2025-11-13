using CompanyPost.Application.DTO.Response.Base;
using CompanyPost.Application.Extension;

namespace CompanyPost.Application.CQRS.Handlers.Query.GetPostInternalDocuments
{
	internal class GetPostInternalDocumentsHandler :
		IRequestHandler<GetPostInternalDocumentsQuery, IEnumerable<PostDocumentsDTO>>
	{
		private readonly IUnitOfWork _unitOfWork;
		public GetPostInternalDocumentsHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IEnumerable<PostDocumentsDTO>> Handle(GetPostInternalDocumentsQuery request, CancellationToken cancellationToken)
		{
			var postRepository = _unitOfWork.Repository<PostInternal>();

			var includes = new List<Expression<Func<PostInternal, object>>>
				 {
					 post => post.CreatedBy,
					 post => post.Publisher,
					 post => post.RecievedFrom,
					 post => post.WorkType,
					 post => post.Company,
					 post => post.Attachments,
				 };

            var predicate = PredicateBuilder.New<PostInternal>(true);

            if (request.BaseDocumentFilterRequestDTO.StartDate.HasValue)
            {
                predicate = predicate.And(p => p.DocumentDate >= request.BaseDocumentFilterRequestDTO.StartDate.Value);
            }
            if (request.BaseDocumentFilterRequestDTO.EndDate.HasValue)
            {
                predicate = predicate.And(p => p.DocumentDate <= request.BaseDocumentFilterRequestDTO.EndDate.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.BaseDocumentFilterRequestDTO.DocumentNumber))
            {
                predicate = predicate.And(p => p.DocumentNumber == request.BaseDocumentFilterRequestDTO.DocumentNumber);
            }

            var posts = await postRepository.FindWithIncludeAsync(predicate, includes, cancellationToken);

			var postDTOs = posts.Select(p => new PostDocumentsDTO(
				p.Id,
				p.SerialNumber,
				p.DocumentNumber,
				p.DocumentDate.ToString("dd-MM-yyyy"),
				p.DeliveryDate.ToString("dd-MM-yyyy"),
				p.Attachments != null && p.Attachments.Any()
					? p.Attachments.Select(a => $"/posts/{a.FileName}").ToList()
					: new List<string>(),
				p.Subject,
				p.Summary,
				p.Notes,
				p.CreatedBy.UserName,
				p.Publisher.Name,
				p.DeliveryMethods.GetDisplayName(),
				p.Company.Name,
				p.WorkType.Name,
				p.RecievedFrom.Name,
				p.Department.GetDisplayName()
			));

			return postDTOs;
		}
	}
}
