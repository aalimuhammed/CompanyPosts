namespace CompanyPost.Domain.Entities;
public class SysUsersCompany : BaseEntity
{
	public Guid SysUserId { get; set; }
	public SysUsers SysUser { get; set; } = null!;
	public Guid CompanyId { get; set; }
	public Company Company { get; set; } = null!;
}