namespace CompanyPost.Application.CQRS.Commands.SysUser;
public record CreateSysUserCompanyCommand
	(CreateSysUserCompanyDTO CreateSysUserCompanyDTO) : IRequest<Unit>;