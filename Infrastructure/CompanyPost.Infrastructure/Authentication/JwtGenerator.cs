namespace CompanyPost.Infrastructure.Authentication;
internal class JwtGenerator : IJwTGenerator
{
	private readonly JwtSettings _jwtSettings;
	public JwtGenerator(IOptions<JwtSettings> jwtSettings)
	{
		_jwtSettings = jwtSettings.Value;
	}
	public string CreateToken(SysUsers sysUsers , int? expirationInMinutes = null)
	{
		var claims = new List<Claim>
		{
			  new Claim(JwtRegisteredClaimNames.Sub, sysUsers.Id.ToString()),
			  new Claim(JwtRegisteredClaimNames.UniqueName, sysUsers.Name),
			  new Claim(JwtRegisteredClaimNames.Email, sysUsers.Email),
			  new Claim("username", sysUsers.Name),

			  new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
		var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: _jwtSettings.Issuer,
			audience: _jwtSettings.Audience,
			claims: claims,
			expires: DateTime.Now.AddMinutes(expirationInMinutes ?? _jwtSettings.ExpirationInMinutes),
			signingCredentials: credentials
		);
		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}