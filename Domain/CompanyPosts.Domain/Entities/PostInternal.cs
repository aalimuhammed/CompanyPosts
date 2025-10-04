namespace CompanyPost.Domain.Entities;
public class PostInternal : PostBaseEntity
{
	public ICollection<PostInternalAttachment> Attachments { get; set; } = new List<PostInternalAttachment>();
}