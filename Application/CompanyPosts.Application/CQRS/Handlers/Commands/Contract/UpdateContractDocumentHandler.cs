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

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var contract = await GetContractAsync(
                    contractRepo,
                    request.Id,
                    request.UpdateContractDocumentDTO.Attachments?.Any() == true,
                    cancellationToken);

                if (contract is null)
                    throw new Exception("Contract Record not found");

                MapContract(contract, request.UpdateContractDocumentDTO);

                if (request.UpdateContractDocumentDTO.Attachments?.Any() == true)
                {
                    await ReplaceAttachmentsAsync(
                        contract,
                        request.UpdateContractDocumentDTO.Attachments!,
                        cancellationToken);
                }

                contractRepo.Update(contract);
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
        private static async Task<Contracts?> GetContractAsync(
            IGenericRepository<Contracts> contractRepo,
            Guid contractId,
            bool includeAttachments,
            CancellationToken cancellationToken)
        {
            if (!includeAttachments)
            {
                return await contractRepo.FindAsync(
                    x => x.Id == contractId,
                    cancellationToken);
            }

            Expression<Func<Contracts, object>>[] includes =
            {
                c => c.ContractAttachments
            };

            return (await contractRepo.FindWithIncludeAsync(
                    x => x.Id == contractId,
                    includes,
                    cancellationToken))
                .FirstOrDefault();
        }
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

            contract.Currency = (Currency) dto.Currency;
        }
        private async Task ReplaceAttachmentsAsync(
            Contracts contract,
            List<IFormFile> newAttachments,
            CancellationToken cancellationToken)
        {
            DeleteExistingAttachments(contract);
            await AddAttachmentsAsync(contract.Id, newAttachments, cancellationToken);
        }
        private void DeleteExistingAttachments(Contracts contract)
        {
            if (!contract.ContractAttachments.Any())
                return;

            var attachmentRepo = _unitOfWork.Repository<ContractAttachments>();

            foreach (var attachment in contract.ContractAttachments)
            {
                if (!string.IsNullOrWhiteSpace(attachment.FileName))
                {
                    _fileService.DeleteFile("contracts", attachment.FileName);
                    attachmentRepo.Delete(attachment);
                }
            }
            contract.ContractAttachments.Clear();
        }
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
        #endregion
    }
}
