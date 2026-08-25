using CompanyPost.Application.CQRS.Commands.PurchaseOrder;

namespace CompanyPost.Application.CQRS.Handlers.Commands.PurchaseOrders
{
    internal sealed class DeletePurchaseOrderHandler
         : IRequestHandler<DeletePurchaseOrderCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        public DeletePurchaseOrderHandler(
            IUnitOfWork unitOfWork, 
            IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }
        public async Task<bool> Handle(DeletePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var purchaseOrderRepository = _unitOfWork.Repository<PurchaseOrder>();
            var purchaseOrderAttachmentRepository = _unitOfWork.Repository<PurchaseOrderAttachment>();

            Expression<Func<PurchaseOrder, object>>[] includes = { po => po.PurchaseOrderAttachments };

            // Begin transaction
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var purchaseOrders = await purchaseOrderRepository
                    .FindWithIncludeAsync(po => po.Id == request.Id , includes , cancellationToken);

                var purchaseOrder = purchaseOrders?.FirstOrDefault();

                if (purchaseOrder == null)
                {
                    throw new KeyNotFoundException($"Purchase Order with Id {request.Id} not found.");
                }

                // Delete associated attachments (if any)
                if (purchaseOrder.PurchaseOrderAttachments?.Any() == true)
                {
                    foreach (var attachment in purchaseOrder.PurchaseOrderAttachments)
                    {
                        if (!string.IsNullOrWhiteSpace(attachment.FileName))
                        {
                            _fileService.DeleteFile("purchaseorders", attachment.FileName);
                        }

                        purchaseOrderAttachmentRepository.Delete(attachment);
                    }
                }

                // Delete the purchase order itself
                purchaseOrderRepository.Delete(purchaseOrder);

                // Persist changes
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // Commit transaction
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return true;
            }
            catch
            {
                // Rollback in case of failure
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
