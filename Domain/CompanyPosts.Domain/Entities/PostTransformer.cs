namespace CompanyPost.Domain.Entities;
public class PostTransformer : PostBaseEntity
{ 
	public ICollection<PostTransformerAttachment> Attachments { get; set; } = new List<PostTransformerAttachment>();
}