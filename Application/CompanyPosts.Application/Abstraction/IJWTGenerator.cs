namespace CompanyPost.Application.Abstraction;
public interface IJwTGenerator
{
	string CreateToken(SysUsers sysUsers , int? expirationInMinutes = null);
}