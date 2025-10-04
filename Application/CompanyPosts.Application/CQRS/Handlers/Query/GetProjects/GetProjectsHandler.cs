namespace CompanyPost.Application.CQRS.Handlers.Query.GetProjects;
internal sealed class GetProjectsHandler
	: IRequestHandler<GetProjectsQuery, IEnumerable<ProjectsResponseDTO>>
{
	private readonly IUnitOfWork _unitOfWork;
	public GetProjectsHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<IEnumerable<ProjectsResponseDTO>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
	{
		var projectRepository = _unitOfWork.Repository<Publisher>();

		var projects = await projectRepository.FindAllAsync(
			x => x.IsProject,
			cancellationToken);

		var projectDTOs = projects.Select(p => new ProjectsResponseDTO(
			p.Name,
			p.Id
		));
		return projectDTOs;
	}
}