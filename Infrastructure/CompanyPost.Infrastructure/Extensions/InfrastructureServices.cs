namespace CompanyPost.Infrastructure.Extensions;
public static class InfrastructureServices
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services, 
		IConfiguration conguration)
	{
		var defaultConnectionString = conguration.GetConnectionString("DefaultConnection");

		services.AddDbContext<CompanyPostDbContext>(
			options => options.UseMySql(defaultConnectionString,
			ServerVersion.AutoDetect(defaultConnectionString))
			.UseSnakeCaseNamingConvention());

		services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		services.AddScoped<IUnitOfWork , UnitOfWork>();
		services.AddScoped<IFileService, FileService>();

		services.Configure<JwtSettings>(conguration.GetSection("JwtSettings"));
		services.AddSingleton<IJWTGenerator, JwtGenerator>();

		var JwtSettings = conguration.GetSection("JwtSettings").Get<JwtSettings>();

		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings!.Secret)),
				ValidateIssuer = true,
				ValidIssuer = JwtSettings.Issuer,
				ValidateAudience = true,
				ValidAudience = JwtSettings.Audience,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			};
		});
		services.AddAuthorization();

		services.AddSingleton<IPasswordService, PasswordServices>();

		return services;
	}
}
