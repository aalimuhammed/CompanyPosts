namespace CompanyPost.Domain.Entities;
public class BridgeUsers : BaseEntity
{
	public string Name { get; private set; } = null!;
	public string Username { get; private set; } = null!;
	public string Email { get; private set; } = null!;
	public string Password { get; private set; } = null!;
}