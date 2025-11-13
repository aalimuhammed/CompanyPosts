using CompanyPost.Application.DTO.Response.Base;
using CompanyPost.Application.Extension;

namespace CompanyPost.Application.CQRS.Handlers.Query.GetInComingDocuments
{
	internal class GetInComingDocumentsHandler
		: IRequestHandler<GetInComingDocumentsQuery, IEnumerable<PostDocumentsDTO>>
	{
		private readonly IUnitOfWork _unitOfWork;
		public GetInComingDocumentsHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IEnumerable<PostDocumentsDTO>> Handle(GetInComingDocumentsQuery request, CancellationToken cancellationToken)
		{
			var inComingRepository = _unitOfWork.Repository<InComing>();

			var includes = new List<Expression<Func<InComing, object>>>
					 {
						 post => post.CreatedBy,
						 post => post.Publisher,
						 post => post.Projects,
						 post => post.WorkType,
						 post => post.OriginalPublisher,
						 post => post.IncomingAttachments,
					 };

            var predicate = PredicateBuilder.New<InComing>(true);

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

            var inComing = await inComingRepository.FindWithIncludeAsync(predicate, includes, cancellationToken);

			var inComingDto = inComing.Select(p => new PostDocumentsDTO(
				p.Id,
				p.SerialNumber,
				p.DocumentNumber,
				p.DocumentDate.ToString("dd-MM-yyyy"),
				p.DeliveryDate.ToString("dd-MM-yyyy"),
				p.IncomingAttachments != null && p.IncomingAttachments.Any()
					? p.IncomingAttachments.Select(a => $"/incoming/{a.FileName}").ToList()
					 : new List<string>(),
				p.Subject,
				p.Summary,
				p.Notes,
				p.CreatedBy.UserName,
				p.Publisher.Name,
				p.DeliveryMethods.GetDisplayName(),
				null,
				p.WorkType.Name,
				p.OriginalPublisher.Name,
				p.Department.GetDisplayName()
			));
			return inComingDto;
		}
	}
}
