namespace CompanyPost.Application.Abstraction;
public interface IFileService
{
	Task<string> SaveAttachmentAsync(IFormFile attachment, string folderName,CancellationToken cancellationToken);
	void DeleteFile(string folderName, string fileName);
}