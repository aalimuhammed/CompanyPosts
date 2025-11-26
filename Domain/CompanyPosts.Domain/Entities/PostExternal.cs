namespace CompanyPost.Domain.Entities;
public class PostExternal : PostBaseEntity
{
    //public string IncomingNumber { get; set; } = null!;
	public Guid ReceivedFromSupplierId { get; set; }
	public Publisher ReceivedFromSupplier { get; set; } = null!;
	public Guid WorkTypeId { get; set; }
	public WorkType WorkType { get; set; } = null!;

    public ICollection<PostExternalAttachment> Attachments = new List<PostExternalAttachment>();
}