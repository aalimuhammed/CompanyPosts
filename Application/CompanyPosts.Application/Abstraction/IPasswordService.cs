namespace CompanyPost.Application.Abstraction;
public interface IPasswordService
{
	string HashPassword(string password);
	bool VerifyPassword(string password, string hashedPassword);
	string GenerateRandomPassword();
}