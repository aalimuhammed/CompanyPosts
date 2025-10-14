using CompanyPost.Application.CQRS.Commands.SysUser;

namespace CompanyPost.Application.CQRS.Handlers.Commands.SysUser;
internal sealed class CreateSysUserCompanyHandler
	: IRequestHandler<CreateSysUserCompanyCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IPasswordService _passwordService;
	public CreateSysUserCompanyHandler(
		IUnitOfWork unitOfWork, 
		IPasswordService passwordService)
	{
		_unitOfWork = unitOfWork;
		_passwordService = passwordService;
	}
	public async Task<Unit> Handle(CreateSysUserCompanyCommand request, CancellationToken cancellationToken)
	{
		var sysUserRepository = _unitOfWork.Repository<SysUsers>();
		var sysUserComapnyRepository = _unitOfWork.Repository<SysUsersCompany>();
		try
		{
			await _unitOfWork.BeginTransactionAsync();
			var plainPassword = _passwordService.GenerateRandomPassword();

			var sysUser = new SysUsers
			{
				Email = request.CreateSysUserCompanyDTO.Email,
				UserName = request.CreateSysUserCompanyDTO.UserName,
				Name = request.CreateSysUserCompanyDTO.Name,
				Password = _passwordService.HashPassword(plainPassword)
			};

			await sysUserRepository.AddAsync(sysUser);
			foreach (var item in request.CreateSysUserCompanyDTO.Companies)
			{
				var sysUserCompany = new SysUsersCompany
				{
					SysUserId = sysUser.Id,
					CompanyId = item
				};
				await sysUserComapnyRepository.AddAsync(sysUserCompany);
			}

			await _unitOfWork.SaveChangesAsync();
			await _unitOfWork.CommitTransactionAsync();
		}
		catch (Exception)
		{
			await _unitOfWork.RollbackTransactionAsync();
			throw;
		}

		return Unit.Value;
	}
}