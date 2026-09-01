using CompanyPost.Infrastructure.Settings;
using Microsoft.Extensions.Logging;

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
			.UseSnakeCaseNamingConvention()
			.EnableSensitiveDataLogging()
			.LogTo(Console.WriteLine , LogLevel.Information));

		services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        //services.Configure<HostSettings>(configuration.GetSection("Hosts"));

		services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		services.AddScoped(typeof(ICurrencyRepository<>), typeof(CurrencyRepository<>));
		services.AddScoped<IUnitOfWork , UnitOfWork>();
		services.AddScoped<IFileService, FileService>();
		services.AddScoped<IEmailServices, EmailServices>();
		services.AddScoped<IGetCurrentUserTokenService, GetCurrentUserTokenService>();

		services.AddSingleton<IJwTGenerator, JwtGenerator>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IPasswordService, PasswordServices>();

		var jwtSection = configuration.GetSection("JwtSettings");
		services.Configure<JwtSettings>(jwtSection);
		var jwtSettings = jwtSection.Get<JwtSettings>();

		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{
			options.MapInboundClaims = false;
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
		//services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
  //          services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
  //          services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));
            return services;
	}
}
