using CompanyPost.Application.CQRS.Commands.InComing;

namespace CompanyPost.Application.CQRS.Handlers.Commands.InComings
{
    internal sealed class UpdateInComingDocumentHandler : IRequestHandler<UpdateInComingDocumentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
		private readonly IFileService _fileService;
		public UpdateInComingDocumentHandler(
			IUnitOfWork unitOfWork, IFileService fileService)
		{
			_unitOfWork = unitOfWork;
			_fileService = fileService;
		}
		public async Task<bool> Handle(UpdateInComingDocumentCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.Repository<InComing>();

			await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
				var inComing = await GetInComingAsync(
					repository,
					request.Id,
					request.UpdateInComingDocumentRequest.Attachments?.Any() == true,
					cancellationToken);

				if (inComing is null)
				{
					throw new Exception("InComing Record not found");
				}

				inComing.DeliveryMethods = (DeliveryMethods)request.UpdateInComingDocumentRequest.deliveryMethod;
				inComing.PostDocumentTypes = (PostDocumentTypes)request.UpdateInComingDocumentRequest.department;
				inComing.DocumentNumber = request.UpdateInComingDocumentRequest.documentNumber;
				inComing.DocumentDate = request.UpdateInComingDocumentRequest.documentDate;
				inComing.WorkTypeId = request.UpdateInComingDocumentRequest.workTypeId;
				inComing.Subject = request.UpdateInComingDocumentRequest.subject;
				inComing.Notes = request.UpdateInComingDocumentRequest.notes;
				inComing.Summary = request.UpdateInComingDocumentRequest.summary;
				inComing.DeliveryDate = request.UpdateInComingDocumentRequest.deliveryDate;
				inComing.PublishedId = request.UpdateInComingDocumentRequest.publishedId;
				inComing.ProjectId = request.UpdateInComingDocumentRequest.projectId;
				inComing.OriginalSender = request.UpdateInComingDocumentRequest.originalsender;
				inComing.DocumentType = (DocumentType)request.UpdateInComingDocumentRequest.documentType;

				if (request.UpdateInComingDocumentRequest.Attachments?.Any() == true)
				{
					await ReplaceAttachmentsAsync(
						inComing,
						request.UpdateInComingDocumentRequest.Attachments!,
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
        private static async Task<InComing?> GetInComingAsync(
            IGenericRepository<InComing> repository,
            Guid inComingId,
			bool includeAttachments,
			CancellationToken cancellationToken)
        {
			if (!includeAttachments)
			{
				return await repository.FindAsync(
					x => x.Id == inComingId,
					cancellationToken);
			}

			Expression<Func<InComing, object>>[] includes =
			{
				c => c.IncomingAttachments
			};

			return (await repository.FindWithIncludeAsync(
					x => x.Id == inComingId,
					includes,
					cancellationToken))
				.FirstOrDefault();
		}
		private async Task ReplaceAttachmentsAsync(
		   InComing inComing,
		   List<IFormFile> newAttachments,
		   CancellationToken cancellationToken)
		{
			DeleteExistingAttachments(inComing);
			await AddAttachmentsAsync(inComing.Id, newAttachments, cancellationToken);
		}
		private void DeleteExistingAttachments(InComing inComing)
		{
			if (!inComing.IncomingAttachments.Any())
				return;

			var attachmentRepo = _unitOfWork.Repository<InComingAttachments>();

			foreach (var attachment in inComing.IncomingAttachments)
			{
				if (!string.IsNullOrWhiteSpace(attachment.FileName))
				{
					_fileService.DeleteFile("incomings", attachment.FileName);
					attachmentRepo.Delete(attachment);
				}
			}
			inComing.IncomingAttachments.Clear();
		}
		private async Task AddAttachmentsAsync(
			Guid inComingId,
			List<IFormFile> attachments,
			CancellationToken cancellationToken)
		{
			var attachmentRepo = _unitOfWork.Repository<InComingAttachments>();

			foreach (var file in attachments)
			{
				var fileName = await _fileService.SaveAttachmentAsync(
					file,
					"incomings",
					cancellationToken);

				await attachmentRepo.AddAsync(new InComingAttachments
				{
					IncomingId = inComingId,
					FileName = fileName
				}, cancellationToken);
			}
		}
	}
}