namespace CompanyPost.Application.DTO.Request;
public record CreateSysUserCompanyDTO(
	string UserName,
	string Password,
	string Name,
	string Email ,
	string HrCode,
	Guid Company);