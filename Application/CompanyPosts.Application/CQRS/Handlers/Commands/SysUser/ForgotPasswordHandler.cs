namespace CompanyPost.Application.CQRS.Handlers.Commands.SysUser
{
    internal class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailServices _emailServices;
        private readonly IUrlService _urlService;
        public ForgotPasswordHandler(
            IUnitOfWork unitOfWork,
            IEmailServices emailServices,
            IUrlService urlService)
        {
            _unitOfWork = unitOfWork;
            _emailServices = emailServices;
            _urlService = urlService;
        }
        public async Task<bool> Handle(ForgotPasswordCommand request,
            CancellationToken cancellationToken)
        {
            var email = request.forgotPasswordDto.Email;
            if(string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email cannot be empty");
            }
            var userRepo = _unitOfWork.Repository<SysUsers>();

            var user = await userRepo.FindAsync(x => x.Email == email, cancellationToken);

            if (user == null)
            {
                throw new ArgumentException("No active user found with the provided email");
            }
            // we consider the new guid as a token for password reset,
            // and we set the expiration time for the token to be 10 min from now.
            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("=", "\n");
            
            user.ResetPasswordExpires= DateTime.UtcNow.AddMinutes(10);
            user.ResetPasswordToken = token.Trim();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
             
            var resetLink = $"{_urlService.GetLocalUrl()}resetpassword?token={Uri.EscapeDataString(token.Trim())}";

            var body = $"<p>To reset your password, click the link below (valid for 10 minutes):</p><p><a href=\"{resetLink}\">Reset Password</a></p>";

            _ = _emailServices.SendEmailAsync(user.Email, "Reset your CompanyPost password", body, cancellationToken);

           return true;
        }
    }
}
