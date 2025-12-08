namespace CompanyPost.Domain.Entities;
public class PostTransformer : PostBaseEntity
{
	public string PostNumber { get; set; } = null!;
	public string IncomingNumber { get; set; } = null!;
	public string RecivedByName { get; set; } = null!;
	public string FollowingPerson { get; set; } = null!;
	public Departments Department { get; set; }
	public ICollection<PostTransformerAttachment> Attachments { get; set; } = new List<PostTransformerAttachment>();
}