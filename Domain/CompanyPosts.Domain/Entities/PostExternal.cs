namespace CompanyPost.Domain.Entities;
public class PostExternal : PostBaseEntity
{
	public string IncomingNumber { get; set; } = null!;
	public ICollection<PostExternalAttachment> Attachments = new List<PostExternalAttachment>();
}
