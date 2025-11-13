using CompanyPost.Application.CQRS.Commands.SysUser;

namespace CompanyPost.Application.CQRS.Handlers.Commands.SysUser;
internal sealed class CreateSysUserCompanyHandler
	: IRequestHandler<CreateSysUserCompanyCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IPasswordService _passwordService;
	private readonly IEmailServices _emailServices;
	public CreateSysUserCompanyHandler(
		IUnitOfWork unitOfWork, 
		IPasswordService passwordService,
		IEmailServices emailServices)
	{
		_unitOfWork = unitOfWork;
		_passwordService = passwordService;
		_emailServices = emailServices;
	}
	public async Task<Unit> Handle(CreateSysUserCompanyCommand request, CancellationToken cancellationToken)
	{
		var sysUserRepository = _unitOfWork.Repository<SysUsers>();
		var sysUserCompanyRepository = _unitOfWork.Repository<SysUsersCompany>();
		try
		{
            bool hrCodeExists  = await sysUserRepository.FindAnyAsync(
				u => u.HrCode == request.CreateSysUserCompanyDTO.HrCode, cancellationToken);
            if (hrCodeExists) throw new InvalidOperationException("Hr Code already exists.");

            bool emailExists = await sysUserRepository.FindAnyAsync(
				u => u.Email == request.CreateSysUserCompanyDTO.Email, cancellationToken);
            if (emailExists) throw new InvalidOperationException("Email already exists.");

            bool userNameExists = await sysUserRepository.FindAnyAsync(
				u => u.UserName == request.CreateSysUserCompanyDTO.UserName, cancellationToken);
            if (userNameExists) throw new InvalidOperationException("Username already exists.");

            await _unitOfWork.BeginTransactionAsync(cancellationToken);
			var plainPassword = _passwordService.GenerateRandomPassword();

			var sysUser = new SysUsers
			{
				Email = request.CreateSysUserCompanyDTO.Email,
				UserName = request.CreateSysUserCompanyDTO.UserName,
				Name = request.CreateSysUserCompanyDTO.Name,
				Password = _passwordService.HashPassword(plainPassword),
				HrCode = request.CreateSysUserCompanyDTO.HrCode
            };

			await sysUserRepository.AddAsync(sysUser);
			foreach (var item in request.CreateSysUserCompanyDTO.Companies)
			{
				var sysUserCompany = new SysUsersCompany
				{
					SysUserId = sysUser.Id,
					CompanyId = Guid.Parse(item)
				};
				await sysUserCompanyRepository.AddAsync(sysUserCompany);
			}

			await _unitOfWork.SaveChangesAsync(cancellationToken);
			await _unitOfWork.CommitTransactionAsync(cancellationToken);
            _= SendWelcomeEmailAsync(sysUser.Email, cancellationToken);
        }
        catch (InvalidOperationException)
        {
            throw; 
        }
        catch (Exception ex)
		{
			await _unitOfWork.RollbackTransactionAsync(cancellationToken);
			throw;
		}

		return Unit.Value;
	}
    private async Task SendWelcomeEmailAsync(string recipientEmail, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(recipientEmail)) return;

        var subject = "Welcome to Company Post";
        var html = @"
				<p>Dear User,</p>
				<p>Your account has been successfully created.</p>
				<p>Please use the link below to log in:</p>
				<p><a href='http://192.168.121.29:3000/login' target='_blank'>Click here to access your account</a></p>
				<p>Best regards,<br/>The Support Team</p>";

        try
        {
            await _emailServices.SendEmailAsync(recipientEmail, subject, html, cancellationToken);
        }
		catch(Exception ex)
		{
			throw new InvalidOperationException("An Error occured while sending email for new users" , ex);
		}
	}
}