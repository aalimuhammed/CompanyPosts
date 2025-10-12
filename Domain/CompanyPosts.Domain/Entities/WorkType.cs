namespace CompanyPost.Domain.Entities;
public class WorkType : BaseEntity, IHasName
{
	public string Name { get; set; } = null!;
	public string Code { get; set; } = null!;
	public ICollection<Contracts> Contracts = new List<Contracts>();
}