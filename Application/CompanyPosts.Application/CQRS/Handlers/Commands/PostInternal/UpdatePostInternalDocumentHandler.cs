using CompanyPost.Application.Helpers;
using CompanyPost.Domain.Entities;

namespace CompanyPost.Application.CQRS.Handlers.Commands.PostInernals
{
    internal sealed class UpdatePostInternalDocumentHandler
         : IRequestHandler<UpdatePostInternalDocumentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
		private readonly AttachmentsHelper _attachmentsHelper;
        public UpdatePostInternalDocumentHandler(
			IUnitOfWork unitOfWork , 
			IFileService fileService)
        {
            _unitOfWork = unitOfWork;
			_attachmentsHelper = new AttachmentsHelper(unitOfWork, fileService);
		}
        public async Task<bool> Handle(UpdatePostInternalDocumentCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.Repository<PostInternal>();
			await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var dto = request.UpdatePostInternalDocumentRequestDTO;

                var hasNewFiles = dto.Attachments?.Any() == true;
                var idsToDelete = dto.AttachmentIdsToDelete ?? new List<Guid>();
                var hasDeletions = idsToDelete.Any();
                var needsAttachmentsLoaded = hasNewFiles || hasDeletions;

                var postInternal = await repository.GetByIdAsyncWithAttachmentIncluded(
							 request.Id,
                             needsAttachmentsLoaded,
							 x => x.Attachments,
							 cancellationToken);

				if (postInternal is null)
				{
					throw new Exception($"Post External with ID '{request.Id}' is not found");
				}

				postInternal.DeliveryMethods = (DeliveryMethods)request.UpdatePostInternalDocumentRequestDTO.deliveryMethod;
				postInternal.DocumentNumber = request.UpdatePostInternalDocumentRequestDTO.documentNumber;
				postInternal.DocumentDate = request.UpdatePostInternalDocumentRequestDTO.documentDate;
				postInternal.PostDocumentTypes = (PostDocumentTypes)request.UpdatePostInternalDocumentRequestDTO.department;
				postInternal.RecievedFromId = request.UpdatePostInternalDocumentRequestDTO.receivedFromId;
				postInternal.WorkTypeId = request.UpdatePostInternalDocumentRequestDTO.workTypeId;
				postInternal.Subject = request.UpdatePostInternalDocumentRequestDTO.subject;
				postInternal.Notes = request.UpdatePostInternalDocumentRequestDTO.notes;
				postInternal.Summary = request.UpdatePostInternalDocumentRequestDTO.summary;
				postInternal.DeliveryDate = request.UpdatePostInternalDocumentRequestDTO.deliveryDate;
				postInternal.PublishedId = request.UpdatePostInternalDocumentRequestDTO.publishedId;
				postInternal.CompanyId = request.UpdatePostInternalDocumentRequestDTO.companyId;
				postInternal.Status = (Status)request.UpdatePostInternalDocumentRequestDTO.status;
                postInternal.OldReferenceNumber = request.UpdatePostInternalDocumentRequestDTO.oldReferenceNumber;
				postInternal.InComingNumber = request.UpdatePostInternalDocumentRequestDTO.inComingNumber;

                if (hasDeletions)
                {
                    var toRemove = postInternal.Attachments
                        .Where(a => idsToDelete.Contains(a.Id))
                        .ToList();

                    foreach (var att in toRemove)
                    {
                        _unitOfWork.Repository<PostInternalAttachment>().Delete(att);
                        postInternal.Attachments.Remove(att);
                    }
                }

                if (hasNewFiles)
                {
                    await _attachmentsHelper.AppendAsync(
                        postInternal.Attachments,
                        request.UpdatePostInternalDocumentRequestDTO.Attachments!,
                        "posts",
                        a => a.FileName,
                        a => _unitOfWork
                            .Repository<PostInternalAttachment>()
                            .Delete(a),
                        fileName => new PostInternalAttachment
                        {
                            PostInternalId = postInternal.Id,
                            FileName = fileName
                        },
                        cancellationToken);
                }

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