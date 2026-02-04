namespace CompanyPost.Application.CQRS.Query
{
	public record GetContractRefMaxSerialNumberQuery(Guid contractId) : IRequest<int>;
}