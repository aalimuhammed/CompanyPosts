namespace CompanyPost.Application.CQRS.Query
{
	public record GetContractsNumbersQuery : IRequest<IEnumerable<ContractNumberDTO>>;
}
