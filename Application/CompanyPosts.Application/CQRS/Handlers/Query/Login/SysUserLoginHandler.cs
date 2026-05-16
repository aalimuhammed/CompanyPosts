using System.ComponentModel.DataAnnotations;

namespace CompanyPost.Application.CQRS.Handlers.Query.Login;
internal class SysUserLoginHandler : IRequestHandler<SysUserLoginQuery, AuthResultDTO>
{
	private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordService _passwordService;
	private readonly IJwTGenerator _jwtGenerator;
	public SysUserLoginHandler(
		IUnitOfWork unitOfWork,
		IPasswordService passwordService,
		IJwTGenerator jWTGenerator)
	{
		_unitOfWork = unitOfWork;
		_passwordService = passwordService;
		_jwtGenerator = jWTGenerator;
	}
	public async Task<AuthResultDTO> Handle(SysUserLoginQuery request, CancellationToken cancellationToken)
	{
		if (string.IsNullOrWhiteSpace(request.usernameOrEmail)
			|| string.IsNullOrEmpty(request.password))
		{
			throw new ValidationException("Username or Password cannot be empty");
		}

		var userRepo = _unitOfWork.Repository<SysUsers>();

		var user = await userRepo.FindAsync(
			u => u.UserName == request.usernameOrEmail.ToLowerInvariant() || 
			u.Email == request.usernameOrEmail.ToLowerInvariant());

		if (user is null || !_passwordService.VerifyPassword(request.password, user.Password))
		{
			return new AuthResultDTO(false, "Invalid password or email");
		}

		var token = _jwtGenerator.CreateToken(user.Id);
		return new AuthResultDTO(true , "Success" , token , user.UserName);
	}
}