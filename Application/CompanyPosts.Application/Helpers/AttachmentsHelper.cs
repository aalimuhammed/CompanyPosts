using CompanyPost.Domain.Interface;

namespace CompanyPost.Application.Helpers
{
	public sealed class AttachmentsHelper
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IFileService _fileService;
		public AttachmentsHelper(
			IUnitOfWork unitOfWork,
			IFileService fileService)
		{
			_unitOfWork = unitOfWork;
			_fileService = fileService;
		}

		public async Task ReplaceAsync<TAttachment>(
			ICollection<TAttachment> existingAttachments,
			IEnumerable<IFormFile> newFiles,
			string folder,
			Func<TAttachment, string?> fileNameSelector,
			Action<TAttachment> deleteDbRecord,
			Func<string, TAttachment> createAttachment,
			CancellationToken cancellationToken)
			where TAttachment : BaseEntity , IEntity
		{
			// Delete old attachments
			if (existingAttachments.Any())
			{
				foreach (var attachment in existingAttachments)
				{
					var fileName = fileNameSelector(attachment);
					if (!string.IsNullOrWhiteSpace(fileName))
					{
						_fileService.DeleteFile(folder, fileName);
						deleteDbRecord(attachment);
					}
				}

				existingAttachments.Clear();
			}

			var repo = _unitOfWork.Repository<TAttachment>();

			foreach (var file in newFiles)
			{
				var fileName = await _fileService.SaveAttachmentAsync(
					file,
					folder,
					cancellationToken);

				await repo.AddAsync(
					createAttachment(fileName),
					cancellationToken);
			}
		}
	}
}
