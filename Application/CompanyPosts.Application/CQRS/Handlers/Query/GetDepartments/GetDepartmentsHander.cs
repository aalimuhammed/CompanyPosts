namespace CompanyPost.Application.CQRS.Handlers.Query.GetDepartments;
internal sealed class GetDepartmentsHander
	: IRequestHandler<GetDepartmentsQuery, IEnumerable<DepartmentDTO>>
{
	private readonly IUnitOfWork _unitOfWork;
	public GetDepartmentsHander(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<IEnumerable<DepartmentDTO>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
	{
		var publisherRepository = _unitOfWork.Repository<Publisher>();
		var allDepartments = await publisherRepository.FindAllAsync(x => x.IsDepartment , cancellationToken);
		var departmentDTOs = allDepartments.Select(d => new DepartmentDTO(d.Id, d.Name));
		return departmentDTOs;
	}
}