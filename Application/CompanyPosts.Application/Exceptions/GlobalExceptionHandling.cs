using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CompanyPost.Application.Exceptions;
public sealed class GlobalExceptionHandling(
	RequestDelegate next , 
	ILogger<GlobalExceptionHandling> logger)
{
	public async Task InvokeAsync(HttpContext httpContext)
	{
		try
		{
			await next(httpContext);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Unhandled exception occurred");

			httpContext.Response.StatusCode = ex switch
			{
				ApplicationException => StatusCodes.Status400BadRequest,
				_ => StatusCodes.Status500InternalServerError
			};

			await httpContext.Response.WriteAsJsonAsync(
				 new ProblemDetails
				 {
					 Type = ex.GetType().Name,
					 Title = "An error Occurred",
					 Detail = ex.Message
				 });
		}
	}
}
