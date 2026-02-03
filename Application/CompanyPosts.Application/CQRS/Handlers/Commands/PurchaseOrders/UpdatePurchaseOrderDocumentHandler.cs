using CompanyPost.Application.CQRS.Commands.PurchaseOrder;
using CompanyPost.Application.Helpers;

namespace CompanyPost.Application.CQRS.Handlers.Commands.PurchaseOrders
{
    internal sealed class UpdatePurchaseOrderDocumentHandler : IRequestHandler<UpdatePurchaseOrderDocumentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
		private readonly AttachmentsHelper _attachmentsHelper;
		public UpdatePurchaseOrderDocumentHandler(
			IUnitOfWork unitOfWork , 
			IFileService fileService)
        {
            _unitOfWork = unitOfWork;
			_attachmentsHelper = new AttachmentsHelper(unitOfWork, fileService);
		}
        public async Task<bool> Handle(UpdatePurchaseOrderDocumentCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.Repository<PurchaseOrder>();
            try
            {
				var hasAttachments =
						request.UpdateRequestDTO.Attachments?.Any() == true;

				var purchaseOrder  = await repository.GetByIdAsyncWithAttachmentIncluded(
							 request.Id,
							 hasAttachments,
							 x => x.PurchaseOrderAttachments,
							 cancellationToken);

				if (purchaseOrder is null)
				{
					throw new Exception($"Purchase Order With ID {request.Id} is not found");
				}


				purchaseOrder.Department = (Departments)request.UpdateRequestDTO.DepartmentId;
				purchaseOrder.PurchaseOrderNumber = request.UpdateRequestDTO.PurchaseOrderNumber;
				purchaseOrder.Value = request.UpdateRequestDTO.PurchaseOrderValue;
				purchaseOrder.PurchaseOrder_Date = request.UpdateRequestDTO.PurchaseOrderDate;
				purchaseOrder.ProjectId = request.UpdateRequestDTO.ProjectId;
				purchaseOrder.WorkTypeId = request.UpdateRequestDTO.WorkTypeId;
				purchaseOrder.PersonOrgId = request.UpdateRequestDTO.SupplierId;

				if (hasAttachments)
				{
					await _attachmentsHelper.ReplaceAsync(
						purchaseOrder.PurchaseOrderAttachments,
						request.UpdateRequestDTO.Attachments!,
						"purchaseorders",
						a => a.FileName,
						a => _unitOfWork
							.Repository<PurchaseOrderAttachment>()
							.Delete(a),
						fileName => new PurchaseOrderAttachment
						{
							PurchaseOrderId = purchaseOrder.Id,
							FileName = fileName
						},
						cancellationToken);
				}

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