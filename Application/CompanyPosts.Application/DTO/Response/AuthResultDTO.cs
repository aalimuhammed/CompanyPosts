namespace CompanyPost.Application.DTO.Response;
public record AuthResultDTO(
    bool IsSuccess, 
    string Message , 
    string? Token = null , 
    string? UserName = null);