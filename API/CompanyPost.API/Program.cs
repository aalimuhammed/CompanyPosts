using CompanyPost.Application.Exceptions;

namespace CompanyPost.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services
              .AddApplication()
              .AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        try
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider
                    .GetRequiredService<CompanyPostDbContext>();

                context.Database.Migrate();

                await SeedData.Initialize(context);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"An error occurred during migration: {ex.Message}");

            throw;
        }

        app.UseMiddleware<GlobalExceptionHandling>();

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseCors(policy => policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials());

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}