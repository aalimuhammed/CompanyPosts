namespace CompanyPost.Application.CQRS.Handlers.Commands.PostInernals
{
    internal sealed class UpdatePostInternalDocumentHandler
         : IRequestHandler<UpdatePostInternalDocumentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePostInternalDocumentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdatePostInternalDocumentCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.Repository<PostInternal>();
            var postInternal = await repository.FindAsync(x => x.Id == request.Id, cancellationToken);

            if (postInternal == null)
                throw new Exception($"PostInternal with ID '{request.Id}' not found.");

            postInternal.DeliveryMethods = (DeliveryMethods)request.UpdatePostInternalDocumentRequestDTO.deliveryMethod;
            postInternal.DocumentNumber = request.UpdatePostInternalDocumentRequestDTO.documentNumber;
            postInternal.DocumentDate = request.UpdatePostInternalDocumentRequestDTO.documentDate;
           // postInternal.Department = (Departments)request.UpdatePostInternalDocumentRequestDTO.department;
            postInternal.RecievedFromId = request.UpdatePostInternalDocumentRequestDTO.receivedFromId;
            postInternal.WorkTypeId = request.UpdatePostInternalDocumentRequestDTO.workTypeId;
            postInternal.Subject = request.UpdatePostInternalDocumentRequestDTO.subject;
            postInternal.Notes = request.UpdatePostInternalDocumentRequestDTO.notes;
            postInternal.Summary = request.UpdatePostInternalDocumentRequestDTO.summary;
            postInternal.DeliveryDate = request.UpdatePostInternalDocumentRequestDTO.deliveryDate;
            postInternal.PublishedId = request.UpdatePostInternalDocumentRequestDTO.publishedId;
            postInternal.CompanyId = request.UpdatePostInternalDocumentRequestDTO.companyId;


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
