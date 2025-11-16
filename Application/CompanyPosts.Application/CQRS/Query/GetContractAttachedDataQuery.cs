namespace CompanyPost.Application.CQRS.Query
{
    public record GetContractAttachedDataQuery(Guid Id) : IRequest<ContractAttachedDataResponseDTO>;
}