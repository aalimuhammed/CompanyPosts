using CompanyPost.Application.Helpers;

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
                var dto = request.UpdatePostExternalDocumentRequestDTO;

                var hasNewFiles = dto.Attachments?.Any() == true;
                var idsToDelete = dto.AttachmentIdsToDelete ?? new List<Guid>();
                var hasDeletions = idsToDelete.Any();
                var needsAttachmentsLoaded = hasNewFiles || hasDeletions;

                var postExternal = await repository.GetByIdAsyncWithAttachmentIncluded(
			                 request.Id,
                             needsAttachmentsLoaded,
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
				//postExternal.WorkTypeId = request.UpdatePostExternalDocumentRequestDTO.workTypeId;
				postExternal.Subject = request.UpdatePostExternalDocumentRequestDTO.subject;
				postExternal.Notes = request.UpdatePostExternalDocumentRequestDTO.notes;
				postExternal.Summary = request.UpdatePostExternalDocumentRequestDTO.summary;
				postExternal.DeliveryDate = request.UpdatePostExternalDocumentRequestDTO.deliveryDate;
				postExternal.PublishedId = request.UpdatePostExternalDocumentRequestDTO.publishedId;
				postExternal.CompanyId = request.UpdatePostExternalDocumentRequestDTO.companyId;
				postExternal.PostDocumentTypes = (PostDocumentTypes)request.UpdatePostExternalDocumentRequestDTO.department;
                postExternal.Status = (Status)request.UpdatePostExternalDocumentRequestDTO.status;
				postExternal.OldReferenceNumber = request.UpdatePostExternalDocumentRequestDTO.oldReferenceNumber;
				postExternal.InComingNumber = request.UpdatePostExternalDocumentRequestDTO.inComingNumber;

                if (hasDeletions)
                {
                    var toRemove = postExternal.Attachments
                        .Where(a => idsToDelete.Contains(a.Id))
                        .ToList();

                    foreach (var att in toRemove)
                    {
                        _unitOfWork.Repository<PostExternalAttachment>().Delete(att);
                        postExternal.Attachments.Remove(att);
                    }
                }

                if (hasNewFiles)
				{
					await _attachmentsHelper.AppendAsync(
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