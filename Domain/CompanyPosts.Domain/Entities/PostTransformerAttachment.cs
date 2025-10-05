namespace CompanyPost.Domain.Entities;
public class PostTransformerAttachment : BaseEntity
{
	public Guid PostTransformerId { get; set; }
	public PostTransformer PostTransformer { get; set; } = null!;
	public string FileName { get; set; } = null!;
}