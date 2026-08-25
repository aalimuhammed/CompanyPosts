using CompanyPost.Application.CQRS.Commands.PurchaseOrder;

namespace CompanyPost.Application.CQRS.Handlers.Commands.PurchaseOrders
{
    internal sealed class CreatePurchaseOrderHandler
         : IRequestHandler<CreatePurchaseOrderCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _saveAttachment;
        private readonly IEmailServices _emailServices;
        public CreatePurchaseOrderHandler(
            IUnitOfWork unitOfWork, 
            IFileService saveAttachment,
            IEmailServices emailServices)
        {
            _unitOfWork = unitOfWork;
            _saveAttachment = saveAttachment;
            _emailServices = emailServices;
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

                if (request.CreatePurchaseOrderDTO.Attachments != null &&
                    request.CreatePurchaseOrderDTO.Attachments.Any())
                {
                    await AddAttachments(newPurchaseOrder.Id,
                    request.CreatePurchaseOrderDTO.Attachments, cancellationToken);
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                if (request.CreatePurchaseOrderDTO.EmailContent is not null
                        && request.CreatePurchaseOrderDTO.SentEmailsTo is not null)
                {
                    var sysUsers = await systUserRepository.FindAllAsync(
                            x => request.CreatePurchaseOrderDTO.SentEmailsTo.Contains(x.Id),
                            cancellationToken);

                    var emailContent = request.CreatePurchaseOrderDTO.EmailContent
                             .Replace("\r\n", "<br>")
                             .Replace("\n", "<br>");

                    emailContent = $@"
		                    <div dir=""rtl"" style=""text-align: right; font-family: Tahoma, Arial, sans-serif; line-height: 1.8;"">
			                    {emailContent}
		                    </div>";

                    await _emailServices.SendBulkEmailAsync(
                      $"متابعة المستند رقم {request.CreatePurchaseOrderDTO.PurchaseOrderNumber} في  امر التوريد",
                             emailContent,
                              sysUsers.Select(u => u.Email!),
                              cancellationToken);
                }
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
                Department = (Departments)request.CreatePurchaseOrderDTO.Department,
                CommericalRegisterId = request.CreatePurchaseOrderDTO.CommericalRegisterId,
                NatureOfWorks = (NatureOfWorks)request.CreatePurchaseOrderDTO.NatureOfWork,
                ImportingStatus = (ImportingStatus)request.CreatePurchaseOrderDTO.ImportingStatus,
                Status = (Status)request.CreatePurchaseOrderDTO.StatusMethod,
                OldReferenceNumber = request.CreatePurchaseOrderDTO.OldRef
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