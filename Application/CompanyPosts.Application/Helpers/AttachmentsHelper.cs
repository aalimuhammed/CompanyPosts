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
		public async Task AppendAsync<TAttachment>(
			ICollection<TAttachment> existingAttachments,
			IEnumerable<IFormFile> newFiles,
			string folder,
			Func<TAttachment, string?> fileNameSelector,
			Action<TAttachment> deleteDbRecord,
			Func<string, TAttachment> createAttachment,
			CancellationToken cancellationToken)
			where TAttachment : BaseEntity , IEntity
		{
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
        public async Task MergeAsync<TAttachment>(
				ICollection<TAttachment> existingAttachments,
				IEnumerable<Guid>? attachmentIdsToDelete,
				IEnumerable<IFormFile>? newFiles,
				string folder,
				Func<TAttachment, Guid> idSelector,
				Func<TAttachment, string?> fileNameSelector,
				Action<TAttachment> deleteDbRecord,
				Func<string, TAttachment> createAttachment,
				CancellationToken cancellationToken)
				where TAttachment : BaseEntity, IEntity
        {
            var idsToDelete = attachmentIdsToDelete?.ToHashSet() ?? new HashSet<Guid>();

            if (idsToDelete.Count > 0 && existingAttachments.Any())
            {
                var toRemove = existingAttachments
                    .Where(a => idsToDelete.Contains(idSelector(a)))
                    .ToList();

                foreach (var attachment in toRemove)
                {
                    var fileName = fileNameSelector(attachment);
                    if (!string.IsNullOrWhiteSpace(fileName))
                    {
                        _fileService.DeleteFile(folder, fileName);
                    }
                    deleteDbRecord(attachment);
                    existingAttachments.Remove(attachment);
                }
            }

            if (newFiles != null)
            {
                var repo = _unitOfWork.Repository<TAttachment>();
                foreach (var file in newFiles)
                {
                    var fileName = await _fileService.SaveAttachmentAsync(file, folder, cancellationToken);
                    await repo.AddAsync(createAttachment(fileName), cancellationToken);
                }
            }
        }
    }
}
