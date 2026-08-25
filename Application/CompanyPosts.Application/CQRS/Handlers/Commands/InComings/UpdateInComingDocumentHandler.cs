using CompanyPost.Application.CQRS.Commands.InComing;
using CompanyPost.Application.Helpers;

namespace CompanyPost.Application.CQRS.Handlers.Commands.InComings
{
    internal sealed class UpdateInComingDocumentHandler : IRequestHandler<UpdateInComingDocumentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
		private readonly AttachmentsHelper _attachmentsHelper;
        public UpdateInComingDocumentHandler(
			IUnitOfWork unitOfWork, 
			IFileService fileService)
		{
			_unitOfWork = unitOfWork;
            _attachmentsHelper = new AttachmentsHelper(unitOfWork, fileService);
        }
		public async Task<bool> Handle(UpdateInComingDocumentCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.Repository<InComing>();

			await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var dto = request.UpdateInComingDocumentRequest;

                var hasNewFiles = dto.Attachments?.Any() == true;
                var idsToDelete = dto.AttachmentIdsToDelete ?? new List<Guid>();
                var hasDeletions = idsToDelete.Any();
                var needsAttachmentsLoaded = hasNewFiles || hasDeletions;

                var inComing = await repository.GetByIdAsyncWithAttachmentIncluded(
                             request.Id,
                             needsAttachmentsLoaded,
                             x => x.IncomingAttachments,
                             cancellationToken);

                if (inComing is null)
				{
					throw new Exception("InComing Record not found");
				}

				inComing.DeliveryMethods = (DeliveryMethods)request.UpdateInComingDocumentRequest.deliveryMethod;
				inComing.PostDocumentTypes = (PostDocumentTypes)request.UpdateInComingDocumentRequest.department;
				inComing.DocumentNumber = request.UpdateInComingDocumentRequest.documentNumber;
				inComing.DocumentDate = request.UpdateInComingDocumentRequest.documentDate;
				//inComing.WorkTypeId = request.UpdateInComingDocumentRequest.workTypeId;
				inComing.Subject = request.UpdateInComingDocumentRequest.subject;
				inComing.Notes = request.UpdateInComingDocumentRequest.notes;
				inComing.Summary = request.UpdateInComingDocumentRequest.summary;
				inComing.DeliveryDate = request.UpdateInComingDocumentRequest.deliveryDate;
				inComing.PublishedId = request.UpdateInComingDocumentRequest.publishedArea;
				inComing.ProjectId = request.UpdateInComingDocumentRequest.projectId;
				inComing.OriginalSender = request.UpdateInComingDocumentRequest.originalsender;
				inComing.DocumentType = (DocumentType)request.UpdateInComingDocumentRequest.documentType;
				inComing.Status = (Status)request.UpdateInComingDocumentRequest.status;
				inComing.OldReferenceNumber = request.UpdateInComingDocumentRequest.oldReferenceNumber;
				inComing.InComingNumber = request.UpdateInComingDocumentRequest.inComingNumber;

                if (hasDeletions)
                {
                    var toRemove = inComing.IncomingAttachments
                        .Where(a => idsToDelete.Contains(a.Id))
                        .ToList();

                    foreach (var att in toRemove)
                    {
                        _unitOfWork.Repository<InComingAttachments>().Delete(att);
                        inComing.IncomingAttachments.Remove(att);
                    }
                }

                if (hasNewFiles)
                {
                    await _attachmentsHelper.AppendAsync(
                        inComing.IncomingAttachments,
                        request.UpdateInComingDocumentRequest.Attachments!,
                        "incomings",
                        a => a.FileName,
                        a => _unitOfWork
                            .Repository<InComingAttachments>()
                            .Delete(a),
                        fileName => new InComingAttachments
                        {
                            IncomingId = inComing.Id,
                            FileName = fileName
                        },
                        cancellationToken);
                }

                repository.Update(inComing);

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