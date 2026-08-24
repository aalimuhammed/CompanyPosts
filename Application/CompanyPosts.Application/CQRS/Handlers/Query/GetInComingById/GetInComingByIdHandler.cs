using CompanyPost.Application.DTO;

namespace CompanyPost.Application.CQRS.Handlers.Query.GetInComingById
{
    internal sealed class GetInComingByIdHandler 
        : IRequestHandler<GetInComingByIdQuery, SelectedInComingByIdDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetInComingByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<SelectedInComingByIdDTO> Handle(
            GetInComingByIdQuery request, 
            CancellationToken cancellationToken)
        {
            var inComingRepository = _unitOfWork.Repository<InComing>();

            var includes = new List<Expression<Func<InComing, object>>>
                {
                        incoming => incoming.IncomingAttachments,
                };

            var inComing = await inComingRepository.FindWithIncludeFirstOrDefaultAsync(
                p => p.Id == request.Id, 
                includes, 
                cancellationToken);

            if (inComing == null)
            {
                throw new Exception($"InComing Post with Id {request.Id} not found.");
            }

            var selectedInComing = new SelectedInComingByIdDTO(
                inComing.DocumentNumber,
                inComing.Subject,
                inComing.Summary,
                inComing.Notes,
                inComing.OldReferenceNumber,
                inComing.InComingNumber,
                inComing.PublishedId,
                inComing.ProjectId,
                inComing.DocumentDate,
                inComing.DeliveryDate,
                (int)inComing.DeliveryMethods,
                (int)inComing.DocumentType , 
                (int)inComing.PostDocumentTypes ,
                (int)inComing.Status,
                 inComing.IncomingAttachments != null && inComing.IncomingAttachments.Any()
               ? inComing.IncomingAttachments.Select(a => new AttachmentDTO(a.Id, a.FileName!, $"/incomings/{a.FileName}")).ToList()
              : new List<AttachmentDTO>());

            return selectedInComing;
        }
    }
}