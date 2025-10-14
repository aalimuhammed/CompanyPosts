namespace CompanyPost.Domain.Entities;
public class SysUsers : BaseEntity , IHasName
{
	public string Name { get; set; } = string.Empty;
	public string UserName { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public bool IsPasswordDefault { get; set; }
	public ICollection<PostBaseEntity> Posts { get; set; } = new List<PostBaseEntity>();
	public ICollection<Contracts> Contracts { get; set; } = new List<Contracts>();
	public ICollection<InComing> IncomingDocuments { get; set; } = new List<InComing>();
	public ICollection<SysUsersCompany> SysUsersCompanies { get; set; } = new List<SysUsersCompany>();
}