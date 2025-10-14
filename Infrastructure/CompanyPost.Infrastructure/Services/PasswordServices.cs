namespace CompanyPost.Infrastructure.Services;
public class PasswordServices : IPasswordService
{
	private readonly Random random = new Random();
	public string GenerateRandomPassword()
	{
		string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

		int passwordLength = 10;
		string password = "";

		for (int i = 0; i < passwordLength; i++)
		{
			int index = random.Next(characters.Length);
			password += characters[index];
		}
		return password;
	}
	public string HashPassword(string password)
	{
		return BCrypt.Net.BCrypt.HashPassword(password);
	}
	public bool VerifyPassword(string password, string hashedPassword)
	{
		return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
	}
}