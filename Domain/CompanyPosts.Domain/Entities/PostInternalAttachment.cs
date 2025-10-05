namespace CompanyPost.Domain.Entities;
public class PostInternalAttachment : BaseEntity
{
	public Guid PostInternalId { get; set; }
	public PostInternal PostInternal { get; set; } = null!;
	public string FileName { get; set; } = null!;
}