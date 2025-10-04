namespace CompanyPost.Domain.Entities;
public class PostTransformerAttachment : BaseEntity
{
	public Guid PostTransformerId { get; set; }
	public PostExternal PostExternal { get; set; } = null!;
}