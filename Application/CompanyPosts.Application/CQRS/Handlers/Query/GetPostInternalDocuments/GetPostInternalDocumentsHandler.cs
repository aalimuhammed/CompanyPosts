using CompanyPost.Application.Extension;
using CompanyPost.Domain.Result;

namespace CompanyPost.Application.CQRS.Handlers.Query.GetPostInternalDocuments
{
	internal class GetPostInternalDocumentsHandler :
		IRequestHandler<GetPostInternalDocumentsQuery, PaginatedResult<PostDocumentsDTO>>
	{
		private readonly IUnitOfWork _unitOfWork;
		public GetPostInternalDocumentsHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<PaginatedResult<PostDocumentsDTO>> Handle(GetPostInternalDocumentsQuery request, CancellationToken cancellationToken)
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

			var pagedPosts = await postRepository.GetPagedAsync(
				  pageNumber: 1,
				  pageSize: 50,
				  filter: null,
				  includes: includes,
				  orderBy: q => q.OrderByDescending(p => p.DocumentDate),
				  cancellationToken: cancellationToken
			  );

			var postDTOs = pagedPosts.Items.Select(p => new PostDocumentsDTO(
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
			)).ToList();

			return new PaginatedResult<PostDocumentsDTO>(
				items: postDTOs,
				totalCount: pagedPosts.TotalCount,
				pageNumber: 1,
				pageSize: 50
			);
		}
	}
}
