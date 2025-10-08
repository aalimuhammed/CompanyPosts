namespace CompanyPost.Application.CQRS.Handlers.Query.GetProjectsAndDepartments;
internal sealed class GetProjectsAndDepartmentsHandler :
	IRequestHandler<GetProjectsAndDepartmentsQuery, IEnumerable<ProjectsAndDepartmentsResponseDTO>>
{
	private readonly IUnitOfWork _unitOfWork;
	public GetProjectsAndDepartmentsHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<IEnumerable<ProjectsAndDepartmentsResponseDTO>> Handle
		(GetProjectsAndDepartmentsQuery request, 
		CancellationToken cancellationToken)
	{
		var publisherRepository = _unitOfWork.Repository<Publisher>();

		var projectsAndDepartments = await publisherRepository.FindAllAsync(
			x => x.IsProject || x.IsDepartment,
			cancellationToken);

		var projectsAndDepartmentsDTOs = projectsAndDepartments.Select
			(p => new ProjectsAndDepartmentsResponseDTO(
					p.Id , 
					p.Name
		    ));

		return projectsAndDepartmentsDTOs;
	}
}
