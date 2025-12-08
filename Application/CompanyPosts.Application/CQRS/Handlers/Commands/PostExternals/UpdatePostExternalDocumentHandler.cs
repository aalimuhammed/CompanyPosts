namespace CompanyPost.Application.CQRS.Handlers.Commands.PostExternals
{
    internal sealed class UpdatePostExternalDocumentHandler : IRequestHandler<UpdatePostExternalDocumentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePostExternalDocumentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdatePostExternalDocumentCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.Repository<PostExternal>();
            var postExternal = await repository.FindAsync(x => x.Id == request.Id, cancellationToken);

            if (postExternal == null)
                throw new Exception($"PostExternal with ID '{request.Id}' not found.");

            postExternal.DeliveryMethods = (DeliveryMethods)request.UpdatePostExternalDocumentRequestDTO.deliveryMethod;
            postExternal.DocumentNumber = request.UpdatePostExternalDocumentRequestDTO.documentNumber;
            postExternal.DocumentDate = request.UpdatePostExternalDocumentRequestDTO.documentDate;
            //postExternal.Department = (Departments)request.UpdatePostExternalDocumentRequestDTO.department;
            postExternal.RecievedFromId = request.UpdatePostExternalDocumentRequestDTO.receivedFromId;
            postExternal.WorkTypeId = request.UpdatePostExternalDocumentRequestDTO.workTypeId;
            postExternal.Subject = request.UpdatePostExternalDocumentRequestDTO.subject;
            postExternal.Notes = request.UpdatePostExternalDocumentRequestDTO.notes;
            postExternal.Summary = request.UpdatePostExternalDocumentRequestDTO.summary;
            postExternal.DeliveryDate = request.UpdatePostExternalDocumentRequestDTO.deliveryDate;
            postExternal.PublishedId = request.UpdatePostExternalDocumentRequestDTO.publishedId;
            postExternal.CompanyId = request.UpdatePostExternalDocumentRequestDTO.companyId;


            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {

                repository.Update(postExternal);
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
