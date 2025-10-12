namespace CompanyPost.Domain.Base;
public abstract class BaseEntity : IEntity
{
	[Key]
	public Guid Id { get; set; } = Guid.NewGuid();
	public DateTime CreatedAt { get; set; } = DateTime.Now;
}