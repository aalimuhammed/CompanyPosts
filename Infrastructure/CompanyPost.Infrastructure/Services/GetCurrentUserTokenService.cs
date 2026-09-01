using CompanyPost.Application.Exceptions;

namespace CompanyPost.Infrastructure.Services
{
    public class GetCurrentUserTokenService : IGetCurrentUserTokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetCurrentUserTokenService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid UserId { 
            get {
               var userIdClaim = _httpContextAccessor?.HttpContext?.User?
                   .FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (Guid.TryParse(userIdClaim, out var userId))
                {
                    return userId;
                }
                throw new ExpiredTokenException("User ID claim is missing or invalid.");
            } 
        }
    }
}
