using CompanyPost.Application.CQRS.Commands.PurchaseOrder;

namespace CompanyPost.Application.CQRS.Handlers.Commands.PurchaseOrders
{
    internal sealed class CreatePurchaseOrderHandler
         : IRequestHandler<CreatePurchaseOrderCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _saveAttachment;
        public CreatePurchaseOrderHandler(
            IUnitOfWork unitOfWork, IFileService saveAttachment)
        {
            _unitOfWork = unitOfWork;
            _saveAttachment = saveAttachment;
        }
        public async Task<Unit> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var purchaseOrderRepo = _unitOfWork.Repository<PurchaseOrder>();

            var isPurchaseOrderExisting = await purchaseOrderRepo.FindAnyAsync(
                  x => x.PurchaseOrderNumber == request.CreatePurchaseOrderDTO.PurchaseOrderNumber,
                  cancellationToken);

            if (isPurchaseOrderExisting)
            {
                throw new Exception("Cannot have duplicated Purchase Order");
            }

            var systUserRepository = _unitOfWork.Repository<SysUsers>();
            var admin = await systUserRepository.FindAsync(x => x.IsAdmin, cancellationToken);
           
            try
            {
                var newPurchaseOrder = CreatePurchaseOrder(request);
                newPurchaseOrder.CreatedById = admin.Id;

                await _unitOfWork.BeginTransactionAsync();
                await purchaseOrderRepo.AddAsync(newPurchaseOrder,cancellationToken);

                await AddAttachments(newPurchaseOrder.Id,
                    request.CreatePurchaseOrderDTO.Attachments, cancellationToken);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception("An error occurred while creating the purchase order .", ex);
            }

            return Unit.Value;
        }
        private PurchaseOrder CreatePurchaseOrder(CreatePurchaseOrderCommand request)
        {
            return new PurchaseOrder
            {
                SerialNumber = request.CreatePurchaseOrderDTO.SerialNumber,
                Value = request.CreatePurchaseOrderDTO.Value,
                PurchaseOrderNumber = request.CreatePurchaseOrderDTO.PurchaseOrderNumber,
                Details = request.CreatePurchaseOrderDTO.Details,
                Notes = request.CreatePurchaseOrderDTO.Notes,
                PurchaseOrder_Date = request.CreatePurchaseOrderDTO.PurchaseOrderDate,
                WorkTypeId = request.CreatePurchaseOrderDTO.WorkTypeId,
                ProjectId = request.CreatePurchaseOrderDTO.ProjectId,
                PersonOrgId = request.CreatePurchaseOrderDTO.PersonOrgId,
                Currency = (Currency)request.CreatePurchaseOrderDTO.Currency,
                Department = (Departments)request.CreatePurchaseOrderDTO.Department
            };
        }
        private async Task AddAttachments(
            Guid purchaseOrderId,
            List<IFormFile> attachments,
            CancellationToken cancellationToken)
        {
            var attachmentRepository = _unitOfWork.Repository<PurchaseOrderAttachment>();

            foreach (var item in attachments)
            {
                var fileName = await _saveAttachment.SaveAttachmentAsync(
                    item,
                    "purchaseorders",
                    cancellationToken);

                var attachment = new PurchaseOrderAttachment
                {
                    PurchaseOrderId = purchaseOrderId,
                    FileName = fileName,
                };

                await attachmentRepository.AddAsync(attachment, cancellationToken);
            }
        }
    }
}