namespace CompanyPost.Application.CQRS.Commands.SysUser
{
    public record ForgotPasswordCommand(ForgotPasswordDto forgotPasswordDto): IRequest<bool>;
}
