using CompanyPost.Application.Helpers;
using System.Diagnostics.Contracts;

namespace CompanyPost.Application.CQRS.Handlers.Commands.PostExternals
{
    internal sealed class UpdatePostExternalDocumentHandler : IRequestHandler<UpdatePostExternalDocumentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AttachmentsHelper _attachmentsHelper;
        public UpdatePostExternalDocumentHandler(
            IUnitOfWork unitOfWork , 
            IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _attachmentsHelper = new AttachmentsHelper(unitOfWork, fileService);
		}
        public async Task<bool> Handle(UpdatePostExternalDocumentCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.Repository<PostExternal>();
			await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
				var hasAttachments =
			            request.UpdatePostExternalDocumentRequestDTO.Attachments?.Any() == true;

				var postExternal = await repository.GetByIdAsyncWithAttachmentIncluded(
			                 request.Id,
			                 hasAttachments,
			                 x => x.Attachments,
			                 cancellationToken);

                if (postExternal is null)
                {
                    throw new Exception("Post External is not found");
                }

				postExternal.DeliveryMethods = (DeliveryMethods)request.UpdatePostExternalDocumentRequestDTO.deliveryMethod;
				postExternal.DocumentNumber = request.UpdatePostExternalDocumentRequestDTO.documentNumber;
				postExternal.DocumentDate = request.UpdatePostExternalDocumentRequestDTO.documentDate;
				postExternal.RecievedFromId = request.UpdatePostExternalDocumentRequestDTO.receivedFromId;
				postExternal.WorkTypeId = request.UpdatePostExternalDocumentRequestDTO.workTypeId;
				postExternal.Subject = request.UpdatePostExternalDocumentRequestDTO.subject;
				postExternal.Notes = request.UpdatePostExternalDocumentRequestDTO.notes;
				postExternal.Summary = request.UpdatePostExternalDocumentRequestDTO.summary;
				postExternal.DeliveryDate = request.UpdatePostExternalDocumentRequestDTO.deliveryDate;
				postExternal.PublishedId = request.UpdatePostExternalDocumentRequestDTO.publishedId;
				postExternal.CompanyId = request.UpdatePostExternalDocumentRequestDTO.companyId;
				postExternal.PostDocumentTypes = (PostDocumentTypes)request.UpdatePostExternalDocumentRequestDTO.department;

				if (hasAttachments)
				{
					await _attachmentsHelper.ReplaceAsync(
						postExternal.Attachments,
						request.UpdatePostExternalDocumentRequestDTO.Attachments!,
						"posts",
						a => a.FileName,
						a => _unitOfWork
							.Repository<PostExternalAttachment>()
							.Delete(a),
						fileName => new PostExternalAttachment
						{
							PostExternalId = postExternal.Id,
							FileName = fileName
						},
						cancellationToken);

				}

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
