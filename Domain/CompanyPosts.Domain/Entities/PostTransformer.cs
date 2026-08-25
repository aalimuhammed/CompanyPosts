namespace CompanyPost.Domain.Entities;
public class PostTransformer : PostBaseEntity
{
	public string PostNumber { get; set; } = null!;
	public string? IncomingNumber { get; set; } = string.Empty;
	//public string RecivedByName { get; set; } = null!;
	public DocumentType DocumentType { get; set; }
	public ICollection<PostTransformerAttachment> Attachments { get; set; } = new List<PostTransformerAttachment>();
}