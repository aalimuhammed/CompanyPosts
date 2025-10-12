namespace CompanyPost.Domain.Entities;
public class PostExternal : PostBaseEntity , IHasSharedProperty
{
	public string IncomingNumber { get; set; } = null!;
	public Departments Department { get; set; }
	public Guid ReceivedFromSupplierId { get; set; }
	public Publisher ReceivedFromSupplier { get; set; } = null!;
	public ICollection<PostExternalAttachment> Attachments = new List<PostExternalAttachment>();
}
