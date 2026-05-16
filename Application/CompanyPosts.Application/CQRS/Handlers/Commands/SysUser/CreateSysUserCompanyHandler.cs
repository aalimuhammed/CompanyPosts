using CompanyPost.Application.CQRS.Commands.SysUser;
using MediatR;

namespace CompanyPost.Application.CQRS.Handlers.Commands.SysUser;
internal sealed class CreateSysUserCompanyHandler
	: IRequestHandler<CreateSysUserCompanyCommand, Unit>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IPasswordService _passwordService;
	private readonly IEmailServices _emailServices;
	private readonly IJwTGenerator _jwTGenerator;
	public CreateSysUserCompanyHandler(
		IUnitOfWork unitOfWork, 
		IPasswordService passwordService,
		IEmailServices emailServices,
		IJwTGenerator jwTGenerator)
	{
		_unitOfWork = unitOfWork;
		_passwordService = passwordService;
		_emailServices = emailServices;
		_jwTGenerator = jwTGenerator;
	}
	public async Task<Unit> Handle(CreateSysUserCompanyCommand request, CancellationToken cancellationToken)
	{
		var sysUserRepository = _unitOfWork.Repository<SysUsers>();
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

			var sysUser = new SysUsers
			{
				Email = request.CreateSysUserCompanyDTO.Email,
				UserName = request.CreateSysUserCompanyDTO.UserName,
				Name = request.CreateSysUserCompanyDTO.Name,
				Password = _passwordService.HashPassword(request.CreateSysUserCompanyDTO.Password),
				HrCode = request.CreateSysUserCompanyDTO.HrCode,
				CompanyId = request.CreateSysUserCompanyDTO.Company
            };

			await sysUserRepository.AddAsync(sysUser,cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
			await _unitOfWork.CommitTransactionAsync(cancellationToken);
            await SendWelcomeEmailAsync(
				sysUser.Name,
				sysUser.UserName,
				sysUser.HrCode,
				sysUser.Email, 
				cancellationToken);

            _jwTGenerator.CreateToken(sysUser);
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
    private async Task SendWelcomeEmailAsync(
		string name,
		string userName,
		string hrCode,
		string recipientEmail, 
		CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(recipientEmail)) return;

        var subject = "Welcome to Company Post";
        var html = $@"
				<p>Dear {name},</p>
				<p>Please be informed that your account has been successfully created under the username {userName} with Hr Code {hrCode}</p>
				<p>Kindly note that your account is currently pending approval. You will be notified once it has been reviewed and activated by the administrator.</p>
				<p>Thank you for your patience.</p>
				<p>Best regards,<br/>The Software Team</p>";

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