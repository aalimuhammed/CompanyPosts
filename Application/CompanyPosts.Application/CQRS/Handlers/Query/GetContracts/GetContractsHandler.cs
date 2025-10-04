namespace CompanyPost.Application.CQRS.Handlers.Query.GetContracts;
internal sealed class GetContractsHandler
	: IRequestHandler<GetContractsQuery, IEnumerable<ContractResponeDTO>>
{
	private readonly IUnitOfWork _unitOfWork;
	public GetContractsHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<IEnumerable<ContractResponeDTO>> Handle(GetContractsQuery request, CancellationToken cancellationToken)
	{
		var contractRepository = _unitOfWork.Repository<Contracts>();
		var includes = new List<Expression<Func<Contracts, object>>>
			 {
			      contract => contract.Projects,
				  contract => contract.CreatedBy,
				  contract => contract.PersonOrgs
			 };
		var contracts = await contractRepository.FindWithIncludeAsync(null, includes, cancellationToken);
		var contractDTOs = contracts.Select(c => new ContractResponeDTO(
			c.Id,
			c.Value,
			c.ContractNumber,
			c.Details,
			c.Notes,
			c.Contract_Date ,
			c.Projects.Name,
			c.CreatedBy.username,
			c.Currency.ToString(),
			c.working,
			c.purchase_order_ref,
			c.PersonOrgs.Name
			//c.Attachments != null ? $"/contracts/{c.Attachments}" : null
		));
		return contractDTOs;
	}
}