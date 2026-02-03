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
		public async Task<IEnumerable<ContractReportResponseDTO>> Handle(
	GetContractsByFiltersQuery request,
	CancellationToken cancellationToken)
		{
			var contractRepository = _unitOfWork.Repository<Contracts>();
			var contractRefRepository = _unitOfWork.Repository<ContractRef>();

			IEnumerable<ContractReportResponseDTO> contractsResponse;

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

			if (request.DTO.PublisherId.HasValue)
				predicate = predicate.And(c => c.PersonOrgId == request.DTO.PublisherId.Value);

			if (request.DTO.WorkTypeId.HasValue)
				predicate = predicate.And(c => c.WorkTypeId == request.DTO.WorkTypeId.Value);

			if (Enum.TryParse<Departments>(request.DTO.DepartmentId, out var department))
				predicate = predicate.And(c => c.Department == department);

			if (!string.IsNullOrEmpty(request.DTO.ContractRef))
				predicate = predicate.And(c => c.ContractNumber == request.DTO.ContractRef);

			if (!string.IsNullOrEmpty(request.DTO.PurchaseOrderRef))
				predicate = predicate.And(c => c.purchase_order_ref == request.DTO.PurchaseOrderRef);

			if (request.DTO.StartDate.HasValue)
				predicate = predicate.And(c => c.Contract_Date >= request.DTO.StartDate.Value);

			if (request.DTO.EndDate.HasValue)
				predicate = predicate.And(c => c.Contract_Date <= request.DTO.EndDate.Value);

			var contracts = await contractRepository.FindWithIncludeAsync(
				predicate: predicate,
				includes: includes,
				cancellationToken);

			// ✅ CASE 1: Contracts found
			if (contracts.Any())
			{
				contractsResponse = contracts.Select(c => new ContractReportResponseDTO(
					c.Id,
					c.Projects.Name,
					c.ContractNumber,
					c.HasReference
						? c.SerialNumber.ToString()
						: $"{c.ContractNumber}-{c.SerialNumber}",
					c.WorkType.Name,
					c.Contract_Date.ToString("yyyy-MM-dd"),
					c.Department.GetDisplayName(),
					c.purchase_order_ref,
					c.Currency.GetDisplayName(),
					c.PersonOrgs.Name,
					c.HasReference ? "Attached" : "Original",
					c.CreatedBy.UserName,
					c.CreatedAt.ToString("yyyy-MM-dd"),
					c.Value,
					c.ContractAttachments?.Select(a => $"/contracts/{a.FileName}").ToList()
						?? new List<string>()
				));
			}
			// ✅ CASE 2: No contracts → fallback to ContractRef
			else
			{

				var Refincludes = new List<Expression<Func<ContractRef, object>>>
						{
							contract => contract.CreatedBy,
							contract => contract.ContractAttachments
						};
				var predicateRef = PredicateBuilder.New<ContractRef>(true);

				if (!string.IsNullOrEmpty(request.DTO.ContractRef))
					predicateRef = predicateRef.And(c => c.ContractNumber == request.DTO.ContractRef);

				if (request.DTO.StartDate.HasValue)
					predicateRef = predicateRef.And(c => c.Contract_Date >= request.DTO.StartDate.Value);

				if (request.DTO.EndDate.HasValue)
					predicateRef = predicateRef.And(c => c.Contract_Date <= request.DTO.EndDate.Value);

				var contractRefs = await contractRefRepository.FindWithIncludeAsync(
					predicate: predicateRef,
					includes: Refincludes,
					cancellationToken);

				contractsResponse = contractRefs.Select(c => new ContractReportResponseDTO(
					c.Id,
					"",
					c.ContractNumber,
					$"{c.ContractNumber}-{c.SerialNumber}",
					"",
					c.Contract_Date.ToString("yyyy-MM-dd"),
					"",
					"",
					c.Currency.GetDisplayName(),
					"",
					"Attached",
					c.CreatedBy.UserName,
					c.CreatedAt.ToString("yyyy-MM-dd"),
					c.Value,
					c.ContractAttachments?.Select(a => $"/contracts/{a.FileName}").ToList()
						?? new List<string>()
				));
			}

			return contractsResponse;
		}

	}
}