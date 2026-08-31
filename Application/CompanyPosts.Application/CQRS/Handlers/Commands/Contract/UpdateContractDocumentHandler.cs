namespace CompanyPost.Application.CQRS.Handlers.Commands.Contract
{
    internal sealed class UpdateContractDocumentHandler : IRequestHandler<UpdateContractDocumentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        public UpdateContractDocumentHandler(
            IUnitOfWork unitOfWork,
            IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }
        public async Task<bool> Handle(UpdateContractDocumentCommand request, CancellationToken cancellationToken)
        {
            var contractRepo = _unitOfWork.Repository<Contracts>();
            var contractRefRepo = _unitOfWork.Repository<ContractRef>();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var dto = request.UpdateContractDocumentDTO;

                var hasNewFiles = dto.Attachments?.Any() == true;
                var idsToDelete = dto.AttachmentIdsToDelete ?? new List<Guid>();
                var hasDeletions = idsToDelete.Any();
                var needsAttachmentsLoaded = hasNewFiles || hasDeletions;

                var contract = await contractRepo.GetByIdAsyncWithAttachmentIncluded(
                    request.Id, needsAttachmentsLoaded, x => x.ContractAttachments, cancellationToken);

                if (contract != null)
                {
                    MapContract(contract, dto);

                    if (hasDeletions)
                        DeleteSelectedAttachments(contract.ContractAttachments, idsToDelete);

                    if (hasNewFiles)
                        await AddAttachmentsAsync(contract.Id, dto.Attachments!, cancellationToken);

                    contractRepo.Update(contract);
                }
                else
                {
                    var contractRef = await contractRefRepo.GetByIdAsyncWithAttachmentIncluded(
                        request.Id, needsAttachmentsLoaded, x => x.ContractAttachments, cancellationToken);

                    if (contractRef is null)
                        throw new Exception("Contract Record not found");

                    MapContractRef(contractRef, dto);

                    if (hasDeletions)
                        DeleteSelectedAttachments(contractRef.ContractAttachments, idsToDelete);

                    if (hasNewFiles)
                        await AddContractRefAttachmentsAsync(contractRef.Id, dto.Attachments!, cancellationToken);

                    contractRefRepo.Update(contractRef);
                }
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return true;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        #region Private Helpers
        private static void MapContract(
            Contracts contract,
            UpdateContractDocumentRequestDTO dto)
        {
            contract.Value = dto.ContractValue;
            contract.ContractNumber = dto.ContractNumber;
            contract.Details = dto.Details;
            contract.Notes = dto.Notes;
            contract.Contract_Date = dto.ContractDate;
            contract.ProjectId = dto.ProjectId;
            contract.purchase_order_ref = dto.PurchaseOrderRef;
            contract.OldReferenceNumber = dto.OldReferenceNumber;
            contract.PersonOrgId = dto.SupplierId;
            contract.WorkTypeId = dto.WorkTypeId;
            contract.ApprovalDeliveryDate = dto.ApprovalDeliveryDate;
            contract.DateOfReceipt = dto.DateOfReceipt;
            contract.Currency = (Currency)dto.Currency;
            contract.Department = (Departments)dto.Department;
        }

        // Deletes only the attachments whose Id is in idsToDelete — everything
        // else in the collection is left untouched. Works for both Contracts
        // and ContractRef since ContractAttachments is the shared entity type.
        private void DeleteSelectedAttachments(
            ICollection<ContractAttachments> attachments,
            List<Guid> idsToDelete)
        {
            if (!attachments.Any())
                return;

            var attachmentRepo = _unitOfWork.Repository<ContractAttachments>();

            var toRemove = attachments
                .Where(a => idsToDelete.Contains(a.Id))
                .ToList();

            foreach (var attachment in toRemove)
            {
                if (!string.IsNullOrWhiteSpace(attachment.FileName))
                    _fileService.DeleteFile("contracts", attachment.FileName);

                attachmentRepo.Delete(attachment);
                attachments.Remove(attachment);
            }
        }

        // Appends new files without touching existing attachments.
        private async Task AddAttachmentsAsync(
            Guid contractId,
            List<IFormFile> attachments,
            CancellationToken cancellationToken)
        {
            var attachmentRepo = _unitOfWork.Repository<ContractAttachments>();

            foreach (var file in attachments)
            {
                var fileName = await _fileService.SaveAttachmentAsync(
                    file,
                    "contracts",
                    cancellationToken);

                await attachmentRepo.AddAsync(new ContractAttachments
                {
                    ContractID = contractId,
                    FileName = fileName
                }, cancellationToken);
            }
        }

        private static void MapContractRef(
            ContractRef contractRef,
            UpdateContractDocumentRequestDTO dto)
        {
            contractRef.Value = dto.ContractValue;
            contractRef.ContractNumber = dto.ContractNumber;
            contractRef.Details = dto.Details;
            contractRef.Notes = dto.Notes;
            contractRef.Contract_Date = dto.ContractDate;
            contractRef.Currency = (Currency)dto.Currency;
            contractRef.ApprovalDeliveryDate = dto.ApprovalDeliveryDate;
            contractRef.DateOfReceipt=dto.DateOfReceipt;
        }

        // Appends new files to a ContractRef without touching existing attachments.
        private async Task AddContractRefAttachmentsAsync(
            Guid contractRefId,
            List<IFormFile> newAttachments,
            CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<ContractAttachments>(); // or your ContractRef attachment repo
            foreach (var file in newAttachments)
            {
                var fileName = await _fileService.SaveAttachmentAsync(file, "contracts", cancellationToken);
                await repo.AddAsync(new ContractAttachments // adjust entity/type + FK name for ContractRef
                {
                    ContractRefId = contractRefId,
                    FileName = fileName
                }, cancellationToken);
            }
        }
        #endregion
    }
}