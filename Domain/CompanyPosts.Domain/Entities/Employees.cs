namespace CompanyPost.Domain.Entities;
public class Employees : BaseEntity, IHasName
{
	public string Name { get; set; } = null!;	
	public ICollection<InComingResponsibleEmployee> inComingResponsibleEmployees  = new List<InComingResponsibleEmployee>();
}