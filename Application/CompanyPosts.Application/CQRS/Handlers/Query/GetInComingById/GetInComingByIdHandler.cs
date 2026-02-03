namespace CompanyPost.Application.CQRS.Handlers.Query.GetInComingById
{
    internal sealed class GetInComingByIdHandler : IRequestHandler<GetInComingByIdQuery, SelectedInComingByIdDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetInComingByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<SelectedInComingByIdDTO> Handle(GetInComingByIdQuery request, CancellationToken cancellationToken)
        {
            var inComingRepository = _unitOfWork.Repository<InComing>();

            var inComing = await inComingRepository.FindAsync(p => p.Id == request.Id, cancellationToken);

            if (inComing == null)
            {
                throw new Exception($"InComing Post with Id {request.Id} not found.");
            }

            var selectedInComing = new SelectedInComingByIdDTO(
                inComing.DocumentNumber,
                inComing.Subject,
                inComing.Summary,
                inComing.Notes,
                inComing.PublishedId,
                inComing.WorkTypeId,
                inComing.DocumentDate,
                inComing.DeliveryDate,
                inComing.SaveDate,
                (int)inComing.DeliveryMethods,
                inComing.ProjectId,
                (int)inComing.DocumentType , 
                (int)inComing.PostDocumentTypes);

            return selectedInComing;
        }
    }
}