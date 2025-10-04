namespace CompanyPost.Domain.Entities;
public class Publisher : BaseEntity , IHasName
{
	public string Name { get; set; } = null!;
	public bool IsDepartment { get; set; } = false;
	public bool IsProject { get; set; } = false;
	public bool IsSupplier { get; set; } = false;
}