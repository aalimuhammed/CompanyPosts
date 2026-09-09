namespace CompanyPost.Application.CQRS.Commands.SysUser
{
    public record ResetPasswordCommand(ResetPasswordRequestDto resetPasswordRequestDto): IRequest<bool>;
}
