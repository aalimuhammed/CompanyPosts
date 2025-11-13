namespace CompanyPost.Application.CQRS.Handlers.Commands.PostTransformers
{
    internal sealed class UpdatePostTransformerDocumentHandler : IRequestHandler<UpdatePostTransformerDocumentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePostTransformerDocumentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdatePostTransformerDocumentCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.Repository<PostTransformer>();
            var postTransformer = await repository.FindAsync(x => x.Id == request.Id, cancellationToken);

            if (postTransformer == null)
                throw new Exception($"Post Transformer with ID '{request.Id}' not found.");

            postTransformer.DeliveryMethods = (DeliveryMethods)request.UpdatePostTransformerDocumentRequestDTO.deliveryMethod;
            postTransformer.DocumentNumber = request.UpdatePostTransformerDocumentRequestDTO.documentNumber;
            postTransformer.DocumentDate = request.UpdatePostTransformerDocumentRequestDTO.documentDate;
            postTransformer.Department = (Departments)request.UpdatePostTransformerDocumentRequestDTO.department;
            postTransformer.RecievedFromId = request.UpdatePostTransformerDocumentRequestDTO.receivedFromId;
            postTransformer.WorkTypeId = request.UpdatePostTransformerDocumentRequestDTO.workTypeId;
            postTransformer.Subject = request.UpdatePostTransformerDocumentRequestDTO.subject;
            postTransformer.Notes = request.UpdatePostTransformerDocumentRequestDTO.notes;
            postTransformer.Summary = request.UpdatePostTransformerDocumentRequestDTO.summary;
            postTransformer.DeliveryDate = request.UpdatePostTransformerDocumentRequestDTO.deliveryDate;
            postTransformer.PublishedId = request.UpdatePostTransformerDocumentRequestDTO.publishedId;
            postTransformer.CompanyId = request.UpdatePostTransformerDocumentRequestDTO.companyId;
            postTransformer.RecivedByName = request.UpdatePostTransformerDocumentRequestDTO.recivedByName;
            postTransformer.IncomingNumber = request.UpdatePostTransformerDocumentRequestDTO.inComingNumber;
            postTransformer.PostNumber = request.UpdatePostTransformerDocumentRequestDTO.postNumber;

            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {

                repository.Update(postTransformer);
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
