namespace CompanyPost.Application.CQRS.Query;
public record GetProjectsAndDepartmentsQuery 
	: IRequest<IEnumerable<ProjectsAndDepartmentsResponseDTO>>;