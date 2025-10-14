using static System.Formats.Asn1.AsnWriter;

namespace CompanyPost.API;
public class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllers();
		builder.Services
			  .AddApplication()
			  .AddInfrastructure(builder.Configuration);

		var app = builder.Build();
		try
		{
			using (var scope = app.Services.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<CompanyPostDbContext>();
				context.Database.Migrate();
				await SeedData.Initialize(context);
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred during migration: {ex.Message}");
			throw;
		}

		app.UseHttpsRedirection();
		app.UseCors(policy => policy.AllowAnyHeader()
										.AllowAnyMethod()
										.SetIsOriginAllowed(origin => true)
										.AllowCredentials());

		app.UseAuthentication();
		app.UseAuthorization();
		app.MapControllers();
		app.UseStaticFiles();

		app.Run();
	}
}