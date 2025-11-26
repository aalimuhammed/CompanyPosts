namespace CompanyPost.Domain.Entities;
public class PostTransformer : PostBaseEntity , IReceivedInfo
{
	public string PostNumber { get; set; } = null!;
	public string IncomingNumber { get; set; } = null!;
	public string RecivedByName { get; set; } = null!;
	public string FollowingPerson { get; set; } = null!;
	public Departments Department { get; set; }
	public Guid RecievedFromId { get; set; }
	public Publisher RecievedFrom { get; set; } = null!;
	public Guid WorkTypeId { get; set; }
	public WorkType WorkType { get; set; } = null!;
	public ICollection<PostTransformerAttachment> Attachments { get; set; } = new List<PostTransformerAttachment>();
}