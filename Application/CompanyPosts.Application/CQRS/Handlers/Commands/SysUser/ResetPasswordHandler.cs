namespace CompanyPost.Application.CQRS.Handlers.Commands.SysUser
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        public ResetPasswordHandler(
            IUnitOfWork unitOfWork,
            IPasswordService passwordService)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
        }
        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var token = request.resetPasswordRequestDto.Token.Trim();
            var newPassword= request.resetPasswordRequestDto.NewPassword;
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(newPassword))
            {
                throw new ArgumentException("Token or NewPassword cannot be empty");
            }
            var userRepo = _unitOfWork.Repository<SysUsers>();

            var user = await userRepo.FindAsync(u => u.ResetPasswordToken!.Trim() == token );

            if (user is null || !user.ResetPasswordExpires.HasValue || user.ResetPasswordExpires.Value < DateTime.UtcNow)
            {
                return false;
            }

            user.Password = _passwordService.HashPassword(newPassword);
            user.ResetPasswordToken = null;
            user.ResetPasswordExpires = null;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
