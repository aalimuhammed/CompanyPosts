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


          //  predicate = predicate.And(c => c.CreatedById == IJwTGenerator.DECODE.CurrentUSerID);

            var contracts = await contractRepository.FindWithIncludeAsync(
				predicate: predicate,
				includes: includes,
				cancellationToken);

            if (contracts.Any())
            {
                var contractIdsWithRef = contracts
                    .Where(c => c.HasReference)
                    .Select(c => c.Id)
                    .ToList();

                var refsByContractId = new Dictionary<Guid, List<ContractRef>>();

                if (contractIdsWithRef.Any())
                {
                    var refIncludes = new List<Expression<Func<ContractRef, object>>>
								{
									r => r.CreatedBy,
									r => r.ContractAttachments
								};

                    var refPredicate = PredicateBuilder.New<ContractRef>(r => contractIdsWithRef.Contains(r.ContractId));

                    var allRefs = await contractRefRepository.FindWithIncludeAsync(
                        predicate: refPredicate,
                        includes: refIncludes,
                        cancellationToken);

                    refsByContractId = allRefs
                        .GroupBy(r => r.ContractId)
                        .ToDictionary(g => g.Key, g => g.ToList());
                }

                contractsResponse = contracts.Select(c => new ContractReportResponseDTO(
                    c.Id,
                    c.Projects.Name,
                    c.ContractNumber,
                    c.SerialNumber.ToString(),
                    c.WorkType.Name,
                    c.Contract_Date.ToString("yyyy-MM-dd"),
                    c.Department.GetDisplayName(),
                    c.purchase_order_ref,
                    c.Currency.GetDisplayName(),
                    c.PersonOrgs.Name,
                    "أساسي",
                    c.CreatedBy.UserName,
                    c.CreatedAt.ToString("yyyy-MM-dd"),
                    c.Value,
                    c.ApprovalDeliveryDate,
                    c.DateOfReceipt,
                    c.ContractAttachments?.Select(a => $"/contracts/{a.FileName}").ToList()
                        ?? new List<string>(),
                    c.HasReference && refsByContractId.TryGetValue(c.Id, out var refs)
                        ? refs.Select(r => new ContractRefResponseDTO(
                            r.Id,
                            r.ContractNumber,
                            $"{c.ContractNumber}-{r.SerialNumber}",
                            r.Contract_Date.ToString("yyyy-MM-dd"),
                            r.Value,
                            r.CreatedBy.UserName,
                            r.Currency.GetDisplayName(),
                            r.ContractAttachments?.Select(a => $"/contracts/{a.FileName}").ToList()
                                ?? new List<string>()
                        )).ToList()
                        : new List<ContractRefResponseDTO>()
                ));
            }
            else
			{
				var Refincludes = new List<Expression<Func<ContractRef, object>>>
						{
							contract => contract.CreatedBy,
							contract => contract.ContractAttachments,
							contract => contract.Contract.Projects,
							contract => contract.Contract.WorkType,
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
                            c.Contract.Projects.Name,
                            c.ContractNumber,
                            $"{c.Contract.ContractNumber}-{c.SerialNumber}",
                            c.Contract.WorkType.Name,
                            c.Contract_Date.ToString("yyyy-MM-dd"),
                            c.Contract.Department.GetDisplayName(),
                            "",
                            c.Currency.GetDisplayName(),
                            "",
                            "ملحق",
                            c.CreatedBy.UserName,
                            c.CreatedAt.ToString("yyyy-MM-dd"),
                            c.Value,
                            c.ApprovalDeliveryDate,
                            c.DateOfReceipt,
                            c.ContractAttachments?.Select(a => $"/contracts/{a.FileName}").ToList()
                                ?? new List<string>(),
                            new List<ContractRefResponseDTO>()
                        ));
            }

			return contractsResponse;
		}
	}
}