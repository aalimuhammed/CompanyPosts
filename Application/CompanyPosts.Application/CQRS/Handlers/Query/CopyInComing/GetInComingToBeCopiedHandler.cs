namespace CompanyPost.Application.CQRS.Handlers.Query.CopyInComing
{
    internal class GetInComingToBeCopiedHandler : IRequestHandler<GetInComingToBeCopiedQuery, InComingCopiedFromDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetInComingToBeCopiedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<InComingCopiedFromDTO> Handle(GetInComingToBeCopiedQuery request, CancellationToken cancellationToken)
        {
            var inComingRepo = _unitOfWork.Repository<InComing>();

            var inComingEntity = await inComingRepo.FindAsync(x => x.Id == request.Id);

            var response = new InComingCopiedFromDTO(
                Id: inComingEntity.Id,
               // ProjectId: inComingEntity.ProjectId,
                PublishedId: inComingEntity.PublishedId,
                WorkTypeId: inComingEntity.WorkTypeId,
                PostDocumentTypes: (int)inComingEntity.PostDocumentTypes,
                Subject : inComingEntity.Subject,
                Notes: inComingEntity.Notes,
                DeliveryMethods: (int)inComingEntity.DeliveryMethods,
                Status: (int)inComingEntity.Status);

            return response;
        }
    }
}
