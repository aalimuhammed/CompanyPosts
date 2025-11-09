namespace CompanyPost.Application.DTO.Request;
public record CreateSysUserCompanyDTO(
	string UserName ,
	string Name,
	string Email ,
	string HrCode,
	List<string> Companies);