using CompanyPost.Application.CQRS.Commands.InComing;

namespace CompanyPost.Application.CQRS.Handlers.Commands.InComings
{
    internal sealed class UpdateInComingDocumentHandler : IRequestHandler<UpdateInComingDocumentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateInComingDocumentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdateInComingDocumentCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.Repository<InComing>();
            var postInternal = await repository.FindAsync(x => x.Id == request.Id, cancellationToken);

            if (postInternal == null)
                throw new Exception($"InComing Post with ID '{request.Id}' not found.");

            postInternal.DeliveryMethods = (DeliveryMethods)request.UpdateInComingDocumentRequest.deliveryMethod;
            postInternal.DocumentNumber = request.UpdateInComingDocumentRequest.documentNumber;
            postInternal.DocumentDate = request.UpdateInComingDocumentRequest.documentDate;
           // postInternal.Department = (Departments)request.UpdateInComingDocumentRequest.department;
            postInternal.OriginalPublisherId = request.UpdateInComingDocumentRequest.receivedFromId;
            postInternal.WorkTypeId = request.UpdateInComingDocumentRequest.workTypeId;
            postInternal.Subject = request.UpdateInComingDocumentRequest.subject;
            postInternal.Notes = request.UpdateInComingDocumentRequest.notes;
            postInternal.Summary = request.UpdateInComingDocumentRequest.summary;
            postInternal.DeliveryDate = request.UpdateInComingDocumentRequest.deliveryDate;
            postInternal.PublishedId = request.UpdateInComingDocumentRequest.publishedId;
            postInternal.ProjectId = request.UpdateInComingDocumentRequest.projectId;

            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {

                repository.Update(postInternal);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return false;
                throw;
            }
        }
    }
}
