namespace CompanyPost.Application.Abstraction;
public interface IJwTGenerator
{
	string CreateToken(Guid userId , int? expirationInMinutes = null);
}