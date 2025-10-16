using System.Security.Claims;

namespace CompanyPost.Application.CQRS.Handlers.Query.VerifyToken;
internal sealed class VerifyTokenHandler
	: IRequestHandler<VerifyTokenQuery, VerifyTokenResultDTO>
{
	private readonly IHttpContextAccessor _contextAccessor;
	public VerifyTokenHandler(IHttpContextAccessor contextAccessor)
	{
		_contextAccessor = contextAccessor;
	}
	public async Task<VerifyTokenResultDTO> Handle(VerifyTokenQuery request, CancellationToken cancellationToken)
	{
		var user = _contextAccessor.HttpContext?.User;

		if (user?.Identity is not { IsAuthenticated: true })
			return new VerifyTokenResultDTO (IsValid : false, Guid.Empty , Message :"Invalid or expired token." );

		var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		if (!Guid.TryParse(userIdClaim, out var userId))
			return new VerifyTokenResultDTO(IsValid: false, Guid.Empty, Message: "UserId not in the token.");

		return new VerifyTokenResultDTO(IsValid: true, userId, Message: "Token is valid");
	}
}
