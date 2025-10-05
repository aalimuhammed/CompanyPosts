namespace CompanyPost.Application.CQRS.Handlers.Query.GetPersonOrgs;
internal class GetPersonOrgsHandler
	: IRequestHandler<GetPersonOrgsQuery, IEnumerable<PersonOrgResponseDTO>>
{
	private readonly IUnitOfWork _unitOfWork;
	public GetPersonOrgsHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<IEnumerable<PersonOrgResponseDTO>> Handle(
		GetPersonOrgsQuery request, 
		CancellationToken cancellationToken)
	{
		var personOrgRepository = _unitOfWork.Repository<Publisher>();

		var personOrgs = await personOrgRepository.FindAllAsync(
			x => x.IsSupplierOrSubContractor, 
			cancellationToken);

		var personOrgDTOs = personOrgs.Select(po => new PersonOrgResponseDTO(
			po.Name,
			po.Id
		));
		
		return personOrgDTOs;
	}
}