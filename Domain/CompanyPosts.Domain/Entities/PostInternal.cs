namespace CompanyPost.Domain.Entities;
public class PostInternal : PostBaseEntity , IHasSharedProperty , IReceivedInfo
{
	public Departments Department { get; set; }
	public Guid RecievedFromId { get; set; }
	public Publisher RecievedFrom { get; set; } = null!;
	public ICollection<PostInternalAttachment> Attachments { get; set; } = new List<PostInternalAttachment>();
}