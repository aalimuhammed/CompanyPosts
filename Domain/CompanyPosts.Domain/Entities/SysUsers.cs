namespace CompanyPost.Domain.Entities;
public class SysUsers : BaseEntity , IHasName
{
	public string Name { get; set; } = string.Empty;
	public string username { get; set; } = string.Empty;
	public string password { get; set; } = string.Empty;
	public string email { get; set; } = string.Empty;
	public ICollection<PostBaseEntity> Posts { get; set; } = new List<PostBaseEntity>();
	public ICollection<Contracts> Contracts { get; set; } = new List<Contracts>();
}