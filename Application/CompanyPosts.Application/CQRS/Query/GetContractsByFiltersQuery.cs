namespace CompanyPost.Application.CQRS.Query
{
    public record GetContractsByFiltersQuery(ContractsFilterRequestDTO DTO) 
        : IRequest<IEnumerable<ContractReportResponseDTO>>;
}