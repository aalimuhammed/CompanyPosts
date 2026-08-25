namespace CompanyPost.Application.CQRS.Handlers.Query.GetPurchaseOrderById
{
    internal sealed class GetPurchaseOrderByIdHandler : IRequestHandler<GetPurchaseOrderByIdQuery, PurchaseOrderByIdResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPurchaseOrderByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PurchaseOrderByIdResponseDTO> Handle(GetPurchaseOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var purchaseOrderRepository = _unitOfWork.Repository<PurchaseOrder>();

            var purchaseOrder = await purchaseOrderRepository.FindAsync(x => x.Id == request.Id);

            if (purchaseOrder is null)
            {
                throw new Exception($"Purchase Order with Id {request.Id} not found.");
            }

            var purchaseOrderDTO = new PurchaseOrderByIdResponseDTO(
                purchaseOrder.Id,
                PurchaseOrderNumber: purchaseOrder.PurchaseOrderNumber,
                PurchaseOrderValue: purchaseOrder.Value,
                WorkTypeId: purchaseOrder.WorkTypeId ?? Guid.Empty,
                SupplierId: purchaseOrder.PersonOrgId,
                ProjectId: purchaseOrder.ProjectId,
                PurchaseOrderDate: purchaseOrder.PurchaseOrder_Date?.ToString("yyyy-MM-dd"),
                DepartmentId: (int)purchaseOrder.Department
            );
            return purchaseOrderDTO;
        }
    }
}