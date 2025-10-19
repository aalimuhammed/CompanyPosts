namespace CompanyPost.Infrastructure.Extensions;
public static class InfrastructureServices
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services, 
		IConfiguration configuration)
	{
		var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");

		services.AddDbContext<CompanyPostDbContext>(
			options => options.UseMySql(defaultConnectionString,
			ServerVersion.AutoDetect(defaultConnectionString))
			.UseSnakeCaseNamingConvention());

		services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		services.AddScoped<IUnitOfWork , UnitOfWork>();
		services.AddScoped<IFileService, FileService>();

		services.AddSingleton<IJwTGenerator, JwtGenerator>();
		services.AddTransient<IPasswordService, PasswordServices>();

		var jwtSection = configuration.GetSection("JwtSettings");
		services.Configure<JwtSettings>(jwtSection);
		var jwtSettings = jwtSection.Get<JwtSettings>();

		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings!.Secret)),
				ValidateIssuer = true,
				ValidIssuer = jwtSettings.Issuer,
				ValidateAudience = true,
				ValidAudience = jwtSettings.Audience,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			};
		});
		services.AddAuthorization();
		return services;
	}
}
