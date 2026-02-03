using CompanyPost.Application.Helpers;

namespace CompanyPost.Application.CQRS.Handlers.Commands.PostTransformers
{
    internal sealed class UpdatePostTransformerDocumentHandler 
		: IRequestHandler<UpdatePostTransformerDocumentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AttachmentsHelper _attachmentsHelper;
		public UpdatePostTransformerDocumentHandler(
            IUnitOfWork unitOfWork , 
            IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _attachmentsHelper = new AttachmentsHelper(unitOfWork, fileService);
		}
        public async Task<bool> Handle(UpdatePostTransformerDocumentCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.Repository<PostTransformer>();
			await _unitOfWork.BeginTransactionAsync(cancellationToken);
			try
            {
				var hasAttachments =
					request.UpdatePostTransformerDocumentRequestDTO.Attachments?.Any() == true;

                var postTransformer = await repository.GetByIdAsyncWithAttachmentIncluded(
                         request.Id,
                         hasAttachments,
                         x => x.Attachments,
                         cancellationToken);

				if (postTransformer == null)
					throw new Exception($"Post Transformer with ID '{request.Id}' not found.");

				postTransformer.DeliveryMethods = (DeliveryMethods)request.UpdatePostTransformerDocumentRequestDTO.deliveryMethod;
				postTransformer.DocumentNumber = request.UpdatePostTransformerDocumentRequestDTO.documentNumber;
				postTransformer.DocumentDate = request.UpdatePostTransformerDocumentRequestDTO.documentDate;
				//postTransformer.Department = (Departments)request.UpdatePostTransformerDocumentRequestDTO.department;
				postTransformer.PostDocumentTypes = (PostDocumentTypes)request.UpdatePostTransformerDocumentRequestDTO.department;
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
				postTransformer.DocumentType = (DocumentType)request.UpdatePostTransformerDocumentRequestDTO.documentType;

				if (hasAttachments)
				{
					await _attachmentsHelper.ReplaceAsync(
						postTransformer.Attachments,
						request.UpdatePostTransformerDocumentRequestDTO.Attachments!,
						"posts",
						a => a.FileName,
						a => _unitOfWork
							.Repository<PostTransformerAttachment>()
							.Delete(a),
						fileName => new PostTransformerAttachment
						{
							PostTransformerId = postTransformer.Id,
							FileName = fileName
						},
						cancellationToken);
				}
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
