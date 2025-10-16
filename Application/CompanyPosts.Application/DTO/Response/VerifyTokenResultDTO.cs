namespace CompanyPost.Application.DTO.Response;
public record VerifyTokenResultDTO(bool IsValid , Guid? UserId , string? Message);