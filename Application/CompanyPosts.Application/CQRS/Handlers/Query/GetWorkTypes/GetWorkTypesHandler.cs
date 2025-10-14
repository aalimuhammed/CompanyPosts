namespace CompanyPost.Application.CQRS.Handlers.Query.GetWorkTypes;

internal sealed class GetWorkTypesHandler 
	: IRequestHandler<GetWorkTypesQuery, IEnumerable<WorkTypesResponseDTO>>
{
	private readonly IUnitOfWork _unitOfWork;
	public GetWorkTypesHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<IEnumerable<WorkTypesResponseDTO>> Handle(GetWorkTypesQuery request, CancellationToken cancellationToken)
	{
		var workTypesRepository = _unitOfWork.Repository<WorkType>();
		var workTypes = await workTypesRepository.FindAllAsync(cancellationToken: cancellationToken);
		var workTypesDTO = workTypes.Select(c => new WorkTypesResponseDTO(c.Id, c.Name));
		return workTypesDTO;
	}
}
