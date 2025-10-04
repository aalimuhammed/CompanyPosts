namespace CompanyPost.Domain.Entities;
public class Company : BaseEntity, IHasName
{
	public string CompanyCode { get; set; } = null!;
	public string Name { get; set; } = null!;
	public ICollection<PostBaseEntity> PostBaseEntities { get; set; } = new List<PostBaseEntity>();
}