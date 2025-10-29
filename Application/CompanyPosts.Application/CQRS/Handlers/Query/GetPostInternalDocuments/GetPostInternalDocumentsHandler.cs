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

			var posts = await postRepository.FindWithIncludeAsync(predicate: null, includes, cancellationToken);

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
