using CompanyPost.Application.DTO.Response.Base;
using CompanyPost.Application.Extension;

namespace CompanyPost.Application.CQRS.Handlers.Query.GetPostExternalDocuments
{
	internal sealed class GetPostExternalDocumentsHandler
		: IRequestHandler<GetPostExternalDocumentsQuery, IEnumerable<PostDocumentsDTO>>
	{
		private readonly IUnitOfWork _unitOfWork;
		public GetPostExternalDocumentsHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IEnumerable<PostDocumentsDTO>> Handle(GetPostExternalDocumentsQuery request, CancellationToken cancellationToken)
		{
			var postRepository = _unitOfWork.Repository<PostExternal>();

			var includes = new List<Expression<Func<PostExternal, object>>>
				 {
					 post => post.CreatedBy,
					 post => post.Publisher,
					 post => post.RecievedFrom,
					 post => post.WorkType,
					 post => post.Company,
					 post => post.Attachments,
				 };

            var predicate = PredicateBuilder.New<PostExternal>(true);

			if(request.BaseDocumentFilterRequestDTO.StartDate.HasValue)
			{
				predicate = predicate.And(p => p.DocumentDate >= request.BaseDocumentFilterRequestDTO.StartDate.Value);
            }

            if (request.BaseDocumentFilterRequestDTO.EndDate.HasValue)
            {
                predicate = predicate.And(p => p.DocumentDate <= request.BaseDocumentFilterRequestDTO.EndDate.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.BaseDocumentFilterRequestDTO.DocumentNumber))
            {
                predicate = predicate.And(p => p.DocumentNumber  == request.BaseDocumentFilterRequestDTO.DocumentNumber);
            }

            if (!string.IsNullOrWhiteSpace(request.BaseDocumentFilterRequestDTO.InComingNumber))
            {
                predicate = predicate.And(p => p.InComingNumber == request.BaseDocumentFilterRequestDTO.InComingNumber);
            }

            if (!string.IsNullOrWhiteSpace(request.BaseDocumentFilterRequestDTO.ProjectId))
            {
                predicate = predicate.And(p => p.PublishedId == Guid.Parse(request.BaseDocumentFilterRequestDTO.ProjectId));
            }

            if (!string.IsNullOrWhiteSpace(request.BaseDocumentFilterRequestDTO.ProjectId))
            {
                predicate = predicate.And(p => p.RecievedFromId == Guid.Parse(request.BaseDocumentFilterRequestDTO.ProjectId));
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
				p.RecievedFrom.Name
			));

			return postDTOs;
		}
	}
}