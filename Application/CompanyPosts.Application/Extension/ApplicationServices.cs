namespace CompanyPost.Application.Extension;
public static class ApplicationServices
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		var assembly = typeof(ApplicationServices).Assembly;

		services.AddMediatR(configuration =>
		  configuration.RegisterServicesFromAssembly(assembly));

		services.AddHttpContextAccessor();

		return services;
	}
}