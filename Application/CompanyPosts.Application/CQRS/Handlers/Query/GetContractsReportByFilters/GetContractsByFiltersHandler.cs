using CompanyPost.Application.Extension;


namespace CompanyPost.Application.CQRS.Handlers.Query.GetContractsReportByFilters
{
    internal sealed class GetContractsByFiltersHandler
        : IRequestHandler<GetContractsByFiltersQuery, IEnumerable<ContractReportResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetContractsByFiltersHandler(IUnitOfWork unitOfWork)
        {
              _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ContractReportResponseDTO>> Handle(GetContractsByFiltersQuery request, CancellationToken cancellationToken)
        {
            var contractRepository = _unitOfWork.Repository<Contracts>();

            var includes = new List<Expression<Func<Contracts, object>>>
                     {
                         contract => contract.CreatedBy,
                         contract => contract.PersonOrgs,
                         contract => contract.Projects,
                         contract => contract.WorkType,
                         contract => contract.ContractAttachments
                     };

            var predicate = PredicateBuilder.New<Contracts>(true);

            if (request.DTO.ProjectId.HasValue)
                predicate = predicate.And(c => c.ProjectId == request.DTO.ProjectId.Value);

            if (Enum.TryParse<Departments>(request.DTO.DepartmentId, out var department))
                predicate = predicate.And(c => c.Department == department);

            if (!string.IsNullOrEmpty(request.DTO.PurchaseOrderRef))
                predicate = predicate.And(c => c.purchase_order_ref == request.DTO.PurchaseOrderRef);

            if (request.DTO.StartDate.HasValue)
                predicate = predicate.And(c => c.Contract_Date >= request.DTO.StartDate.Value);

            if (request.DTO.EndDate.HasValue)
                predicate = predicate.And(c => c.Contract_Date <= request.DTO.EndDate.Value);

            var contracts = await contractRepository.FindWithIncludeAsync(
                predicate: predicate ,
                includes: includes , 
                cancellationToken);

            var contractsResponse = contracts.Select(c => new ContractReportResponseDTO(
                    c.Id,
                    c.Projects.Name,
                    c.ContractNumber,
                    c.SerialNumber.ToString(),
                    c.WorkType.Name,
                    c.Value,
                    c.Contract_Date.ToString("yyyy-MM-dd"),
                    c.Department.GetDisplayName(),
                    c.purchase_order_ref,
                    c.Currency.GetDisplayName(),
                    c.PersonOrgs.Name,
                    c.ContractAttachments?.Select(a => $"/contracts/{a.FileName}").ToList() ?? new List<string>()
                ));

            return contractsResponse;
        }
    }
}