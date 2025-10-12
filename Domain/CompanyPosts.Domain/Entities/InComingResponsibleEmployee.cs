namespace CompanyPost.Domain.Entities;
public class InComingResponsibleEmployee : BaseEntity
{
	public Guid InComingId { get; set; }
	public InComing InComing { get; set; } = null!;
	public Guid EmployeeId { get; set; }
	public Employees Employees { get; set; } = null!;
}