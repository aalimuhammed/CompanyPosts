namespace CompanyPost.Application.CQRS.Handlers.Query.GetCompanies;
internal sealed class GetCompaniesHandler : IRequestHandler<GetCompaniesQuery, IEnumerable<CompanyDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	public GetCompaniesHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<IEnumerable<CompanyDto>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
	{
		var companyRepository = _unitOfWork.Repository<Company>();
		var allCompanies = await companyRepository.FindAllAsync(cancellationToken: cancellationToken);
		var companyDtos = allCompanies.Select(c => new CompanyDto(c.Id, c.Name));
		return companyDtos;
	}
}