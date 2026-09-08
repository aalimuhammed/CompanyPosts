namespace CompanyPost.Application.DTO.Request
{
    public record ResetPasswordRequestDto(
        string Token,
        string NewPassword
    );
}
