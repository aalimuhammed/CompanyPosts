using CompanyPost.Application.CQRS.Commands.PurchaseOrder;

namespace CompanyPost.Application.CQRS.Handlers.Commands.PurchaseOrders
{
    internal sealed class UpdatePurchaseOrderDocumentHandler : IRequestHandler<UpdatePurchaseOrderDocumentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePurchaseOrderDocumentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdatePurchaseOrderDocumentCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.Repository<PurchaseOrder>();
            var purchaseOrder = await repository.FindAsync(x => x.Id == request.Id, cancellationToken);

            if (purchaseOrder == null)
                throw new Exception($"Purchase Order with ID '{request.Id}' not found.");

            purchaseOrder.Department = (Departments)request.UpdateRequestDTO.DepartmentId;
            purchaseOrder.PurchaseOrderNumber = request.UpdateRequestDTO.PurchaseOrderNumber;
            purchaseOrder.Value = request.UpdateRequestDTO.PurchaseOrderValue;
            purchaseOrder.PurchaseOrder_Date = request.UpdateRequestDTO.PurchaseOrderDate;
            purchaseOrder.ProjectId = request.UpdateRequestDTO.ProjectId;
            purchaseOrder.WorkTypeId = request.UpdateRequestDTO.WorkTypeId;
            purchaseOrder.PersonOrgId = request.UpdateRequestDTO.SupplierId;

            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {

                repository.Update(purchaseOrder);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return false;
                throw;
            }
        }
    }
}
