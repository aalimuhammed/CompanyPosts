namespace CompanyPost.Application.DTO.Request;
public record CreateSysUserCompanyDTO(
	string UserName ,
	string Name,
	string Email ,
	List<string> Companies);