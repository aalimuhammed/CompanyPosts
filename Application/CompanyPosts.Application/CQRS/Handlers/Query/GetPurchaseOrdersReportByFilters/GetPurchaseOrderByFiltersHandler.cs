using CompanyPost.Application.Extension;

namespace CompanyPost.Application.CQRS.Handlers.Query.GetPurchaseOrdersReportByFilters
{
    internal sealed class GetPurchaseOrderByFiltersHandler
        : IRequestHandler<GetPurchaseOrderByFiltersQuery, IEnumerable<PurchaseOrdersReportResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPurchaseOrderByFiltersHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<PurchaseOrdersReportResponseDTO>> Handle(
            GetPurchaseOrderByFiltersQuery request, 
            CancellationToken cancellationToken)
        {
            var purchaseOrderRepository = _unitOfWork.Repository<PurchaseOrder>();

            var includes = new List<Expression<Func<PurchaseOrder, object>>>
                     {
                         p => p.CreatedBy,
                         p => p.PersonOrgs,
                         p => p.Projects,
                         p => p.WorkType,
                         p => p.PurchaseOrderAttachments
                     };

            var predicate = PredicateBuilder.New<PurchaseOrder>(true);

            if (request.DTO.ProjectId.HasValue)
                predicate = predicate.And(c => c.ProjectId == request.DTO.ProjectId.Value);

            if (Enum.TryParse<Departments>(request.DTO.DepartmentId, out var department))
                predicate = predicate.And(c => c.Department == department);

            if (!string.IsNullOrEmpty(request.DTO.PurchaseOrderRef))
                predicate = predicate.And(c => c.PurchaseOrderNumber == request.DTO.PurchaseOrderRef);

            if (request.DTO.SupplierId.HasValue)
                predicate = predicate.And(c => c.PersonOrgId == request.DTO.SupplierId);

            if (request.DTO.StartDate.HasValue)
                predicate = predicate.And(c => c.PurchaseOrder_Date >= request.DTO.StartDate.Value);

            if (request.DTO.EndDate.HasValue)
                predicate = predicate.And(c => c.PurchaseOrder_Date <= request.DTO.EndDate.Value);


            var purchaseOrders = await purchaseOrderRepository.FindWithIncludeAsync(
                predicate: predicate,
                includes: includes,
                cancellationToken);


            var purchaseOrderResponse = purchaseOrders.Select(c => new PurchaseOrdersReportResponseDTO(
                    c.Id,
                    c.SerialNumber,
                    c.PurchaseOrderNumber,
                    c.PurchaseOrderTypes.GetDisplayName(),
                    c.Projects.Name,
                    c.WorkType.Name,
                    c.PersonOrgs.Name,
                    c.Value,
                    c.Department.GetDisplayName(),
                    c.Currency.GetDisplayName(),
                    c.CreatedBy.Name,
                    c.PurchaseOrder_Date.ToString("dd-MM-yyyy"),
                    c.PurchaseOrderAttachments?.Select(a => $"/purchaseorders/{a.FileName}").ToList() ?? new List<string>()
                ));
            return purchaseOrderResponse;
        }
    }
}