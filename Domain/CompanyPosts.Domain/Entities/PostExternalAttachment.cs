namespace CompanyPost.Domain.Entities;
public class PostExternalAttachment : BaseEntity
{
	public Guid PostExternalId { get; set; }
	public PostExternal PostExternal { get; set; } = null!;
	public string FileName { get; set; } = null!;
}